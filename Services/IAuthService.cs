using Biblioteca.Dto;
using Biblioteca.Models;

public interface IAuthService
{
    Task<User?> AuthenticateUserAsync(string username, string password);
    TokenDto GenerateJwtToken(User user);
}
