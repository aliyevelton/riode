﻿using Microsoft.AspNetCore.Mvc;

namespace Riode.Areas.Admin.Controllers
{
	public class CategoryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
