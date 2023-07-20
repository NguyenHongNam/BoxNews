using BoxNews.Data;
using BoxNews.Models.Domain;
using BoxNews.Service;
using Microsoft.EntityFrameworkCore;

namespace BoxNews.Service{
    public class PostService : IPostService
    {
        private readonly BoxNewDbContext _context;
        public PostService(BoxNewDbContext _context)
        {
            _context = _context;
        }
        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }
        public List<Post> GetPostsByKeyword(string keyword)
        {
            var filteredPosts = _context.Posts
                .Where(p => p.Title.Contains(keyword))
                .ToList();

            return filteredPosts;
        }
    }
}
        