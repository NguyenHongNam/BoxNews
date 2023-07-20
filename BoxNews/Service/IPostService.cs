using BoxNews.Models.Domain;

namespace BoxNews.Service{
    public interface IPostService
    {
        // List<Post> GetAllPosts();
        List<Post> GetPostsByKeyword(string keyword);
        // List<Post> FilterByStatus(string status);
    }
}
        