using BoxNews.Data;
using BoxNews.Models.AccountViewModel;
using BoxNews.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoxNews.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {private readonly BoxNewDbContext _context;

        public AccountController(BoxNewDbContext _context)
        {
            this._context = _context;
        }
        [Area("Admin")]
        //list danh sách
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = _context.Roles.ToList();
            var accounts = await _context.Accounts.Include(o => o.Roles).ToListAsync();
            return View(accounts);
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
        public async Task<IActionResult> Add(AddAccountViewModel addAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                var account = new Account()
                {
                    UserName = addAccountViewModel.UserName,
                    UserPassword = addAccountViewModel.UserPassword,
                    FullName = addAccountViewModel.FullName,
                    Email = addAccountViewModel.Email,
                    RoleName = addAccountViewModel.RoleName
                };
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(addAccountViewModel);
        }
        [Area("Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountID == id);
            if (account != null)
            {
                var viewModel = new UpdateAccountViewModel()
                {
                    AccountID = account.AccountID,
                    UserName = account.UserName,
                    UserPassword = account.UserPassword,
                    FullName = account.FullName,
                    RoleName = account.RoleName,
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
            if (ModelState.IsValid)
            {
                var account = await _context.Accounts.FindAsync(model.AccountID);
                if (account != null)
                {
                    account.UserName = model.UserName;
                    account.UserPassword = model.UserPassword;
                    account.FullName = model.FullName;
                    account.Email = model.Email;
                    account.RoleName = model.RoleName;

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
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountID == id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
