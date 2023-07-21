using BoxNews.Models.Domain;
using BoxNews.Models.PostViewModel;

namespace BoxNews.Service{
    public interface IPostService
    {
        List<Post> GetPostsByKeyword(string keyword);
        List<Post> GetPostsByCategory(int categoryId);
        List<Post> GetPostsByStatus(bool status);
        List<Category> GetAllCategories();
        void UpdatePost(UpdatePostViewModel post);
    }
}
        