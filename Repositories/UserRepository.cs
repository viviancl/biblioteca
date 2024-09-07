using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password)
    {
        return await _context.User
            .FirstOrDefaultAsync(u => u.UserName == username && u.Password! == password);
    }


    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.User.FirstOrDefaultAsync(u => u.UserName == username);
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
