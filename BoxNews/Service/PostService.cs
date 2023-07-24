using BoxNews.Data;
using BoxNews.Models.Domain;
using BoxNews.Models.PostViewModel;
using BoxNews.Service;
using Microsoft.EntityFrameworkCore;

namespace BoxNews.Service
{
    public class PostService : IPostService
    {
        private readonly BoxNewDbContext _context;
        public PostService(BoxNewDbContext dataContext)
        {
            _context = dataContext;
        }
        public List<Category> GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }
        public List<Post> GetPostsByKeyword(string keyword)
        {
            var filteredPosts = _context.Posts
                .Where(p => p.Title.Contains(keyword))
                .ToList();

            return filteredPosts;
        }
        public List<Post> GetPostsByCategory(int categoryId)
        {
            // Trả về danh sách bài viết theo categoryId
            return _context.Posts.Where(post => post.CategoryID == categoryId).ToList();
        }
        public List<Post> GetPostsByStatus(bool status)
        {
            return _context.Posts.Where(post => post.Status == status).ToList();
        }
        public void UpdatePost(UpdatePostViewModel post)
        {
            var existingPost = _context.Posts.Find(post.PostID);
            post.Categories = _context.Categories.ToList();
            var categoryName = post.Categories.Find(x => x.CategoryID == post.CategoryID).CategoryName.ToString();
            if (existingPost != null)
            {
                existingPost.Title = post.Title;
                existingPost.Author = post.Author;
                existingPost.Content = post.Content;
                //if (image != null && image.Length > 0)
                //{
                //    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                //    using (var stream = new FileStream(filePath, FileMode.Create))
                //    {
                //        image.CopyTo(stream);
                //    }
                //    post.ImgSrc = "wwwroot/images/" + fileName;
                //}
                // existingPost.ImgSrc = post.ImgSrc;
                existingPost.Status = post.Status;
                existingPost.CategoryID = post.CategoryID;
                existingPost.CategoryName = categoryName;
                _context.SaveChanges();
            }
        }
    }
}
