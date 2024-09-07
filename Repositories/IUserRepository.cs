using System.Threading.Tasks;
using Biblioteca.Models;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password);
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByUsernameAsync(string username);
}

