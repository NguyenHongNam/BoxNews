using BoxNews.Data;
using BoxNews.Models.Domain;
using BoxNews.Models.PostViewModel;
using Microsoft.Extensions.Logging;
using BoxNews.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BoxNews.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private readonly BoxNewDbContext _context;
        private readonly IPostService _postService;
        public PostController(BoxNewDbContext _context, IPostService postService)
        {
            this._context = _context;
            _postService = postService;
        }
    
        [Area("Admin")]
        //list danh sách
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // var categories = _context.Categories.ToList();
            // var posts = await _context.Posts.Include(o => o.Category).OrderByDescending(o => o.PostID).ToListAsync();
            // var posts = await _context.Posts.OrderByDescending(o => o.PostID).ToListAsync();
            // return View(posts);
           var categories = _context.Categories.ToList();
           var posts = _context.Posts.OrderByDescending(o => o.PostID).ToList();
            var viewModel = new PostsVM
            {
                Categories = categories,
                Posts = posts
            };

            return View(viewModel);
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            var categories = _context.Categories.ToList();
            var viewModel = new AddPostViewModel
            {
                Categories = categories
            };

            return View(viewModel);
        }
        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(AddPostViewModel addPostViewModel, IFormFile image)
        {
            addPostViewModel.Categories = _context.Categories.ToList();
            if (image != null && image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                addPostViewModel.ImgSrc = fileName;
            }
            var categoryName = addPostViewModel.Categories.Find(x => x.CategoryID == addPostViewModel.CategoryID).CategoryName.ToString();
            var post = new Post()
            {
                Title = addPostViewModel.Title,
                CreatedAt = DateTime.Now,
                Author = addPostViewModel.Author,
                CategoryID = addPostViewModel.CategoryID,
                CategoryName = categoryName,
                AccountID = addPostViewModel.AccountID,
                Content = addPostViewModel.Content,
                ImgSrc = addPostViewModel.ImgSrc,
                Status = addPostViewModel.Status
            };
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Area("Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            var categories = _context.Categories.ToList();
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostID == id);
            if(post != null)
            {
                var viewModel = new UpdatePostViewModel()
                {
                    PostID = post.PostID,
                    Title = post.Title,
                    CreatedAt = post.CreatedAt,
                    Author = post.Author,
                    CategoryID = post.CategoryID,
                    AccountID = post.AccountID,
                    Content = post.Content,
                    ImgSrc = post.ImgSrc,
                    Status = post.Status,
                    Categories = categories
                };
                return await Task.Run(() => View("Update", viewModel));
            }
            return RedirectToAction("Index");
        }
        // [Area("Admin")]
        // [HttpPost]
        // public async Task<IActionResult> Update(UpdatePostViewModel model, IFormFile image)
        // {
        //     var categories = _context.Categories.ToList();
        //     var post = await _context.Posts.FindAsync(model.CategoryID);
        //     if(post != null)
        //     {
        //         post.Title = model.Title;
        //         post.Author = model.Author;
        //         post.CategoryID = model.CategoryID;
        //         post.Content = model.Content;
        //         post.Status = model.Status;
        //         if (image != null && image.Length > 0)
        //         {
        //             var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        //             var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
        //             using (var stream = new FileStream(filePath, FileMode.Create))
        //             {
        //                 await image.CopyToAsync(stream);
        //             }
        //             post.ImgSrc = "/images/" + fileName;
        //         }
                
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction("Index");
        //     }
        //     return RedirectToAction("Index");
        // }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Update(UpdatePostViewModel post)
        {
           _postService.UpdatePost(post);
            return RedirectToAction("Index");
        }
        [Area("Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostID == id);
            if(post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult SearchByKeyword(string keyword)
        {
            var filteredPosts = _postService.GetPostsByKeyword(keyword);
            return PartialView("_PostListPartial", filteredPosts);
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult FilterByCategory(int categoryId)
        {
            var posts = _postService.GetPostsByCategory(categoryId);
            return PartialView("_PostListPartial", posts);
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult FilterByStatus(bool status)
        {
            var posts = _postService.GetPostsByStatus(status);
            return PartialView("_PostListPartial", posts);
        }
    }
}
