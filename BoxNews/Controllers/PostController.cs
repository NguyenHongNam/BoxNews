using Microsoft.AspNetCore.Mvc;
using BoxNews.Models.Domain;
using BoxNews.Data;
using Microsoft.EntityFrameworkCore;

namespace BoxNews.Controllers
{
    public class PostController : Controller
    {
        private readonly BoxNewDbContext _context;
        public PostController(BoxNewDbContext context)
        {
            _context = context;
        }
        public IActionResult DomesticNews()
        {
            var DomesticNews = _context.Posts.Where(p => p.Category.CategoryName == "Tin trong nước").ToList();
            return View(DomesticNews);
        }
        public IActionResult InternationalNews()
        {
            var InternationalNews = _context.Posts.Where(p => p.Category.CategoryName == "Tin quốc tế").ToList();
            return View(InternationalNews);
        }
        public IActionResult Detail(int id)
        {
            // var post = _context.Posts.Include(p => p.Category).FirstOrDefault(p => p.PostID == id);
            // return View(post);
            var post = _context.Posts.Include(p => p.Category).Include(p => p.Comments).FirstOrDefault(p => p.PostID == id);
            if (post != null)
            {
                return View(post);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
