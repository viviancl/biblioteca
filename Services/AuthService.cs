using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Biblioteca.Models;
using Biblioteca.Dto;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<User?> AuthenticateUserAsync(string username, string password)
    {
        return await _userRepository.GetUserByUsernameAndPasswordAsync(username, password);
    }

    public TokenDto GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var hours = int.Parse(_configuration["Jwt:ExpirationTimeHours"]!.ToString());

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddHours(hours),
            signingCredentials: credentials);

        var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

        var response = new TokenDto
        {
            User = user.UserName,
            Token = stringToken,
            ExpirationTimeHours = hours
        };

        return response;
    }
}
