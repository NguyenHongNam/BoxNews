using BoxNews.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BoxNews.Models.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BoxNews.Data;

namespace BoxNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BoxNewDbContext _context;
        public HomeController(ILogger<HomeController> logger, BoxNewDbContext _context)
        {
            _logger = logger;
            this._context = _context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = _context.Categories.ToList();
            var popularPosts = _context.Posts.OrderByDescending(p => p.Comments.Count).Take(5).ToList();
            ViewBag.PopularPosts = popularPosts;
            var posts = await  _context.Posts.Include(o => o.Category).ToListAsync();
            return View(posts);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}