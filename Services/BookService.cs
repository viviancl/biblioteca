using System.Collections.Generic;
using System.Threading.Tasks;
using Biblioteca.Models;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<(IEnumerable<Book> Books, int TotalCount)> GetBooksAsync(string? author, int? publishedYear, string? genre, int pageNumber, int pageSize)
    {
        var books = await _bookRepository.GetBooksAsync(author, publishedYear, genre, pageNumber, pageSize);
        var totalCount = await _bookRepository.GetBooksCountAsync(author, publishedYear, genre);

        return (books, totalCount);
    }


}
