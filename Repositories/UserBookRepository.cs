using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;

public class UserBookRepository : IUserBookRepository
{
    private readonly ApplicationDbContext _context;

    public UserBookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserBook>> GetUserBooksAsync(int userId)
    {
        return await _context.UserBook.Where(ub => ub.UserId == userId).ToListAsync();
    }

    public async Task AddUserBookAsync(UserBook userBook)
    {
        _context.UserBook.Add(userBook);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetUserBookCountAsync(int userId)
    {
        return await _context.UserBook.CountAsync(ub => ub.UserId == userId);
    }
}
