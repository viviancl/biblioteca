using Biblioteca.Dto;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    private readonly ILogger<AuthController> _logger;
    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        _logger.LogInformation("Intento de inicio de sesión para el usuario: {UserName}", loginDto.UserName);

        var user = await _authService.AuthenticateUserAsync(loginDto.UserName, loginDto.Password);

        if (user == null)
        {
            _logger.LogWarning("Credenciales inválidas para el usuario: {UserName}", loginDto.UserName);

            return Unauthorized("Credenciales inválidas");
        }

        var token = _authService.GenerateJwtToken(user);

        return Ok(token);
    }
}
