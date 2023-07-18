using BoxNews.Data;
using BoxNews.Models.Domain;
using BoxNews.Models.LoginViewModel;
using BoxNews.Models.RegisterViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BoxNews.Controllers
{
    public class AccountController : Controller
    {
        private readonly BoxNewDbContext _context;

        public AccountController(BoxNewDbContext _context)
        {
            this._context = _context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(await _context.Accounts.AnyAsync(a=>a.UserName == model.UserName))
                {
                    ModelState.AddModelError("UserName", "Username already exists!");
                    return View(model);
                }
                var account = new Account
                {
                    UserName = model.UserName,
                    UserPassword = model.Password.GetHashCode(),
                    Email = model.Email,
                    FullName = model.FullName,
                };
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login","Account");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = model.Password.GetHashCode();
                var account  = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName == model.UserName && a.UserPassword == hashedPassword);  
                if(account != null)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name,account.UserName),
                        new Claim(ClaimTypes.Role,account.RoleID)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Home");   
                }
                ModelState.AddModelError("", "Invalid username or password!");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
