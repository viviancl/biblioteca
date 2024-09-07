using System.Threading.Tasks;
using Biblioteca.Models;

public interface IUserBookRepository
{
    Task<List<UserBook>> GetUserBooksAsync(int userId);
   
    Task AddUserBookAsync(UserBook userBook);
    
    Task<int> GetUserBookCountAsync(int userId);
}
