using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/books")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    // api/books
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] string? author, [FromQuery] int? publishedYear, [FromQuery] string? genre, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (books, totalCount) = await _bookService.GetBooksAsync(author, publishedYear, genre, pageNumber, pageSize);

        var response = new
        {
            TotalItems = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Items = books
        };

        return Ok(response);
    }
}
