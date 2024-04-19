using Microsoft.AspNetCore.Mvc;

namespace Riode.Areas.Admin.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
