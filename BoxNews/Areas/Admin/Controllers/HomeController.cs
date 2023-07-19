using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoxNews.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Authorize("Administrator")]
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
