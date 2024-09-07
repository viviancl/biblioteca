// Services/IUserService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Biblioteca.Models;

public interface IUserService
{
    Task<(bool Success, string Message)> AddBookToUserAsync(int userId, Book newBook);
    
    Task<(IEnumerable<Book> Books, int TotalCount, string? Error)> GetBooksByUserNameAsync(string username, string? genre, int pageNumber, int pageSize);
}
