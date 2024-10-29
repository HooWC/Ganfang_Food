using System.Diagnostics;
using Fried_Rice.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace Fried_Rice.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Home()
		{
			return View();
		}

		public IActionResult About()
		{
			return View();
		}
	}
}