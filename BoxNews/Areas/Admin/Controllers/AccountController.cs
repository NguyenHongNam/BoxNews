using Microsoft.AspNetCore.Mvc;

namespace BoxNews.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
