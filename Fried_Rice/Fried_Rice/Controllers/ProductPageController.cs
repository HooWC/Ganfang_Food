using Microsoft.AspNetCore.Mvc;

namespace Fried_Rice.Controllers
{
	public class ProductPageController : Controller
	{
		public IActionResult MoreProduct(string type)
		{
			ViewBag.Type = type;
			var login = HttpContext.Session.GetString("Login_Success");
			if (login == "true")
				return View();

			return RedirectToAction("Index", "Login");
		}
	}
}
