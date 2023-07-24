using BoxNews.Data;
using BoxNews.Models.CategoryViewModel;
using BoxNews.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoxNews.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BoxNewDbContext _context;

        public CategoryController(BoxNewDbContext _context)
        {
            this._context = _context;
        }
        [Area("Admin")]
        //list danh sách
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Area("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddAccountViewModel addCategoryViewModel)
        {
            if(ModelState.IsValid)
            {
                var category = new Category()
                {
                    CategoryName = addCategoryViewModel.CategoryName,
                    Description = addCategoryViewModel.Description
                };
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(addCategoryViewModel);
        }
        [Area("Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryID == id);
            if(category != null)
            {
                var viewModel = new UpdateAccountViewModel()
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    Description = category.Description
                };
                return await Task.Run(() => View("Update", viewModel));
            }
            return RedirectToAction("Index");
        }
        //update
        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateAccountViewModel model)
        {
            if(ModelState.IsValid)
            {
                var category = await _context.Categories.FindAsync(model.CategoryID);
                if(category != null)
                {
                    category.CategoryName = model.CategoryName;
                    category.Description = model.Description;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Area("Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryID == id);
            if(category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        
    }
}
