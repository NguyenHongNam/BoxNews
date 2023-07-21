using BoxNews.Data;
using BoxNews.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoxNews.Controllers
{
    public class RatingController : Controller
    {
        private readonly BoxNewDbContext _dbContext;
        public RatingController(BoxNewDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]

        public IActionResult AddComment(int postId, string userName, string comments)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.UserName == userName);
            if (account != null)
            {
                var rating = new Rating
                { 
                    PostID = $"{postId}",
                    UserName = account.FullName,
                    Comments = comments,
                    CreateAt = DateTime.Now
                };
                _dbContext.Ratings.Add(rating);
                _dbContext.SaveChanges();
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
