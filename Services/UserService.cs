using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Models;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    private readonly IBookRepository _bookRepository;

    private readonly IUserBookRepository _userBookRepository;

    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, IUserBookRepository userBookRepository, IBookRepository bookRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;

        _userBookRepository = userBookRepository;

        _bookRepository = bookRepository;

        _logger = logger;
    }

    public async Task<(bool Success, string Message)> AddBookToUserAsync(int userId, Book newBook)
    {

        _logger.LogInformation("Añadiendo libro '{Title}' al usuario con ID {UserId}.", newBook.Title, userId);

        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning("Usuario con ID {UserId} no encontrado.", userId);

            return (false, $"Usuario con ID {userId} no encontrado.");
        }

        var book = await _bookRepository.GetBookByTitleAndAuthorAsync(newBook.Title!, newBook.Author!);
        var userBooks = await _userBookRepository.GetUserBooksAsync(userId);

        if (userBooks.Count >= 10)
        {
            _logger.LogWarning("El usuario ha alcanzado el límite de 10 libros.");

            return (false, "El usuario ha alcanzado el límite de 10 libros.");
        }

        if (book != null)
        {
            var existingUserBook = userBooks.FirstOrDefault(ub => ub.BookId == book.Id);
            if (existingUserBook != null)
            {
                _logger.LogWarning("El libro '{Title}' ya está registrado para el usuario con ID {UserId}.", newBook.Title, userId);

                return (false, "El libro ya está registrado para este usuario.");
            }
            await _userBookRepository.AddUserBookAsync(new UserBook { UserId = user.Id, BookId = book.Id });
        }
        else
        {
            await _bookRepository.AddBookAsync(newBook);
            var newBookRegistered = await _bookRepository.GetBookByTitleAndAuthorAsync(newBook.Title!, newBook.Author!);
            if (newBookRegistered != null)
            {
                await _userBookRepository.AddUserBookAsync(new UserBook { UserId = user.Id, BookId = newBookRegistered.Id });
            }
        }

        _logger.LogInformation("Libro '{Title}' registrado exitosamente para el usuario con ID {UserId}.", newBook.Title, userId);

        return (true, "Libro registrado exitosamente.");
    }
    public async Task<(IEnumerable<Book> Books, int TotalCount, string? Error)> GetBooksByUserNameAsync(string username, string? genre, int pageNumber, int pageSize)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
        {
            return (Enumerable.Empty<Book>(), 0, $"Usuario con nombre de usuario {username} no encontrado.");
        }

        var userBooks = await _userBookRepository.GetUserBooksAsync(user.Id);

        var bookIds = userBooks.Select(ub => ub.BookId).ToList();

        var (books, totalCount) = await _bookRepository.GetBooksIdsAsync(bookIds, genre, pageNumber, pageSize);

        return (books, totalCount, null);
    }
}
