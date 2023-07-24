using BoxNews.Data;
using BoxNews.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BoxNews.Controllers
{
    public class RatingController : Controller
    {
        private readonly BoxNewDbContext _context;
        private readonly ILogger<PostController> _logger;

        public RatingController(BoxNewDbContext context)
        {
            _context = context;
        }
        [HttpPost]

        public IActionResult AddComment(int postId, string comments)
        {
            if (User.Identity.IsAuthenticated)
            {

                var currentUser = _context.Accounts.FirstOrDefault(u => u.UserName == User.Identity.Name);


                var comment = new Rating
                {
                    PostID = postId,
                    AccountID = currentUser.AccountID,
                    UserName = currentUser.UserName,
                    Comments = comments,
                    CreateAt = DateTime.Now
                };


                _context.Ratings.Add(comment);
                _context.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }
    }
}

