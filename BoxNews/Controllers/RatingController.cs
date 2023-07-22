using BoxNews.Data;
using BoxNews.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoxNews.Controllers
{
    public class RatingController : Controller
    {
        private readonly BoxNewDbContext _context;
        public RatingController(BoxNewDbContext context)
        {
            _context = context;
        }
        [HttpPost]

        public IActionResult AddComment(int postId, string userName, string comments)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.UserName == userName);
            if (account != null)
            {
                var rating = new Rating
                { 
                    PostID = postId,
                    UserName = account.FullName,
                    Comments = comments,
                    CreateAt = DateTime.Now
                };
                _context.Ratings.Add(rating);
                _context.SaveChanges();
                return RedirectToAction("Detail", new { id = postId });
            }
            else
            {
                // Xử lý khi không tìm thấy tài khoản
                return NotFound();
            }
        }
    }
}
