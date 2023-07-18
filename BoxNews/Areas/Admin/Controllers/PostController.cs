using BoxNews.Data;
using BoxNews.Models.Domain;
using BoxNews.Models.PostViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BoxNews.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
         private readonly BoxNewDbContext _context;
        public PostController(BoxNewDbContext _context)
        {
            this._context = _context;
        }
        [Area("Admin")]
        //list danh sách
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = _context.Categories.ToList();
            var posts = await _context.Posts.Include(o => o.Category).ToListAsync();
            return View(posts);
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
            if(ModelState.IsValid)
            {
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
                var post = new Post()
                {
                    Title = addPostViewModel.Title,
                    CreatedAt = DateTime.Now,
                    Author = addPostViewModel.Author,
                    CategoryID = addPostViewModel.CategoryID,
                    AccountID = addPostViewModel.AccountID,
                    Content = addPostViewModel.Content,
                    ImgSrc = addPostViewModel.ImgSrc,
                    Status = addPostViewModel.Status
                };
                addPostViewModel.Categories = _context.Categories.ToList();
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(addPostViewModel);
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
        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostViewModel model, IFormFile image)
        {
            // var categories = _context.Categories.ToList();
            var post = await _context.Posts.FindAsync(model.CategoryID);
            if(post != null)
            {
                post.Title = model.Title;
                post.Author = model.Author;
                // post.CategoryID = model.CategoryID;  
                post.Content = model.Content;
                post.CreatedAt = DateTime.Now;
                // Kiểm tra xem người dùng đã chọn ảnh mới hay chưa
                // if (image != null)
                // {
                //     // Lưu ảnh vào vị trí trong máy chủ
                //     var imagePath = "wwwroot/images";
                //     using (var stream = new FileStream(imagePath, FileMode.Create))
                //     {
                //         image.CopyTo(stream);
                //     }
                    
                //     // Cập nhật đường dẫn ảnh mới vào thuộc tính ImagePath của sản phẩm
                //     model.ImgSrc = imagePath;
                // }
                // post.ImgSrc = model.ImgSrc;
                post.Status = model.Status;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
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
    }
}
