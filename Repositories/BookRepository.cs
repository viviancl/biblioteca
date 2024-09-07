using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Models;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetBooksAsync(string? author, int? publishedYear, string? genre, int pageNumber, int pageSize)
    {
        var query = _context.Book.AsQueryable();

        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(b => b.Author!.Contains(author));
        }

        if (publishedYear.HasValue)
        {
            query = query.Where(b => b.PublishedYear == publishedYear.Value);
        }

        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(b => b.Genre!.Contains(genre));
        }

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }


    public async Task<(IEnumerable<Book> Books, int TotalCount)> GetBooksIdsAsync(List<int?> bookIds, string? genre, int pageNumber, int pageSize)
    {
        var booksQuery = _context.Book.Where(b => bookIds.Contains(b.Id));

        if (!string.IsNullOrEmpty(genre))
        {
            booksQuery = booksQuery.Where(b => b.Genre!.Contains(genre));
        }

        var totalCount = await booksQuery.CountAsync();
        var books = await booksQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return (books, totalCount);
    }


    public async Task<int> GetBooksCountAsync(string? author, int? publishedYear, string? genre)
    {
        var query = _context.Book.AsQueryable();

        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(b => b.Author!.Contains(author));
        }

        if (publishedYear.HasValue)
        {
            query = query.Where(b => b.PublishedYear == publishedYear.Value);
        }

        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(b => b.Genre!.Contains(genre));
        }

        return await query.CountAsync();
    }

    public async Task AddBookAsync(Book book)
    {
        _context.Book.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task<Book?> GetBookByTitleAndAuthorAsync(string title, string author)
    {
        return await _context.Book.FirstOrDefaultAsync(b => b.Title == title && b.Author == author);
    }
}
