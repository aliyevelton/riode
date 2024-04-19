using Microsoft.AspNetCore.Mvc;

namespace Riode.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
