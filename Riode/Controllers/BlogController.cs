using Microsoft.AspNetCore.Mvc;

namespace Riode.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
