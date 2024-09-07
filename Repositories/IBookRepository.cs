using System.Collections.Generic;
using System.Threading.Tasks;
using Biblioteca.Models;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetBooksAsync(string? author, int? publishedYear, string? genre, int pageNumber, int pageSize);

    Task<(IEnumerable<Book> Books, int TotalCount)> GetBooksIdsAsync(List<int?> bookIds, string? genre, int pageNumber, int pageSize);

    Task<int> GetBooksCountAsync(string? author, int? publishedYear, string? genre);

    Task<Book?> GetBookByTitleAndAuthorAsync(string title, string author);

    Task AddBookAsync(Book book);
}
