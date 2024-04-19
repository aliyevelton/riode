using Microsoft.AspNetCore.Mvc;

namespace Riode.Areas.Admin.Controllers
{
	public class BrandController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
