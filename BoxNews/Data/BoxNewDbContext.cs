using BoxNews.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BoxNews.Data;
public class BoxNewDbContext : DbContext
{
    public BoxNewDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Category> Categories {get; set;}
    public DbSet<Post> Posts {get; set;}
}