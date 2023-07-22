using BoxNews.Data;
using BoxNews.Models.Domain;
using BoxNews.Models.PostViewModel;
using BoxNews.Service;
using Microsoft.EntityFrameworkCore;

namespace BoxNews.Service{
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

            if (existingPost != null)
            {
                existingPost.Title = post.Title;
                existingPost.Author = post.Author;
                existingPost.Content = post.Content;
                // existingPost.ImgSrc = post.ImgSrc;
                existingPost.Status = post.Status;
                // existingPost.CategoryID = post.CategoryID;

                _context.SaveChanges();
            }
        }
    }
}
        