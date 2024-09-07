using Biblioteca.Models;

public interface IBookService
{
    Task<(IEnumerable<Book> Books, int TotalCount)> GetBooksAsync(string? author, int? publishedYear, string? genre, int pageNumber, int pageSize);

    
}
