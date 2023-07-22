using BoxNews.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BoxNews.Data;
public class BoxNewDbContext : DbContext
{
    public BoxNewDbContext(DbContextOptions options) : base(options)
    {

    }
    private readonly BoxNewDbContext _context;

    public BoxNewDbContext(BoxNewDbContext _context)
    {
        _context = _context;
    }

    public DbSet<Category> Categories {get; set;}
    public DbSet<Post> Posts {get; set;}
    public DbSet<Account> Accounts { get; set;}
    public DbSet<Role> Roles { get; set;}
    public DbSet<Rating> Ratings { get; set;}
}