using BoxNews.Data;
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
    }
}
