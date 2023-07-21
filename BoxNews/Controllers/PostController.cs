using Microsoft.AspNetCore.Mvc;
using BoxNews.Models.Domain;
using BoxNews.Data;

namespace BoxNews.Controllers
{
    public class PostController : Controller
    {
        private readonly BoxNewDbContext _context;
        public PostController(BoxNewDbContext context)
        {
            _context = context;
        }
        public IActionResult Detail(int id)
        {
            var post = _context.Posts.Find(id);
            if(post == null)
            {
                return NotFound();
            }
            return View(post);
        }
    }
}
