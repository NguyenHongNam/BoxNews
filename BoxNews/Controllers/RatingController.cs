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

        public IActionResult AddComment(int postId, string comments)
        {
            var listPost = _context.Posts.ToList();
            var accountId = listPost.Find(x => x.PostID == postId).AccountID;
            var account = _context.Accounts.FirstOrDefault(a => a.AccountID == accountId);
            if (account != null)
            {
                var rating = new Rating
                { 
                    PostID = postId,
                    UserName = account.FullName,
                    AccountID = accountId,
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
