using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }    
   
    public DbSet<Book> Book { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<UserBook> UserBook { get; set; }


}
