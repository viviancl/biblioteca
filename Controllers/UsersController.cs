using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // api/users/{id}/books
    [Authorize]
    [HttpPost("{id}/books")]
    public async Task<IActionResult> AddBookToUser(int id, [FromBody] Book newBook)
    {
        var (success, message) = await _userService.AddBookToUserAsync(id, newBook);

        if (!success)
        {
            return BadRequest(message);
        }

        return Ok(new { Message = message });
    }

    // api/users/{username}/books
    [Authorize]
    [HttpGet("{username}/books")]
    public async Task<IActionResult> GetBooksByUserName(string username, [FromQuery] string? genre, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (books, totalCount, error) = await _userService.GetBooksByUserNameAsync(username, genre, pageNumber, pageSize);
        if (!string.IsNullOrEmpty(error))
        {
            return NotFound(error);
        }

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
