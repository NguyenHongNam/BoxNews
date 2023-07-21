using BoxNews.Models.Domain;
namespace BoxNews.Models.PostViewModel
{
    public class PostsVM
    {
        public List<Category> Categories { get; set; }
        public List<Post> Posts { get; set; }
    }
}