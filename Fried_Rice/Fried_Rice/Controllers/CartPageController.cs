using Fried_Rice_DomainModelEntity.Models;
using Fried_Rice_Insfrastructure.Repository.Api;
using Microsoft.AspNetCore.Mvc;

namespace Fried_Rice.Controllers
{
	public class CartPageController : Controller
	{
		public static ProductFood_Api pro_api = new ProductFood_Api();
		public static Cart_Api cart_api = new Cart_Api();

		public IActionResult CartPay()
		{
			var login = HttpContext.Session.GetString("Login_Success");
			if (login == "true")
				return View();

			return RedirectToAction("Index", "Login");
		}

		public IActionResult CartAdd(int id)
		{
			//Food
			var db = pro_api.GetProductFood(id).Result;

			//cus_noid
			string cus_noid = HttpContext.Session.GetString("UserIdNo");

			//checking cart status
			var data = cart_api.GetAllCart().Result;
			if (data.Count > 0)
			{
				string CartNoid_old = data.LastOrDefault().CardIdNo;
				int CartNoid_new = Convert.ToInt32(CartNoid_old.Substring(3)) + 1;
				string new_noid = "Cd-" + CartNoid_new;

				//有data 搜寻
				Cart cart_data = new Cart();
				foreach (var i in data)
				{
					if (i.CustomersIdNo == cus_noid && i.Status == false && i.ProductFoodIdNo == db.ProductFoodIdNo)
					{
						cart_data = i;
					}
				}

				if (cart_data.CustomersIdNo != null)
				{
					// 加 1
					cart_data.Quantity += 1;
					cart_api.EditCart(cart_data);
					return RedirectToAction("CartPay", "CartPage");
				}
				else
				{
					//新 cart
					Cart cart = new Cart()
					{
						CardIdNo = new_noid,
						ProductFoodIdNo = db.ProductFoodIdNo,
						Quantity = 1,
						Status = false,
						CustomersIdNo = cus_noid
					};

					cart_api.CartCreate(cart);
					return RedirectToAction("CartPay", "CartPage");
				}

			}
			else
			{
				//没有cart 第一个
				Cart cart = new Cart()
				{
					CardIdNo = "Cd-1",
					ProductFoodIdNo = db.ProductFoodIdNo,
					Quantity = 1,
					Status = false,
					CustomersIdNo = cus_noid
				};

				cart_api.CartCreate(cart);
				return RedirectToAction("CartPay", "CartPage");

			}
		}





	}
}
