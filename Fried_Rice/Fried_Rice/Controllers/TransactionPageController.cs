using Fried_Rice_DomainModelEntity.Models;
using System.Collections.Generic;
using Fried_Rice_Insfrastructure.Repository.Api;
using Microsoft.AspNetCore.Mvc;

namespace Fried_Rice.Controllers
{
	public class TransactionPageController : Controller
	{
		public ProductFood_Api pro_api = new ProductFood_Api();
		public Cart_Api cart_api = new Cart_Api();
		public Customers_Api cur_api = new Customers_Api();
		public Transaction_Api tr_api = new Transaction_Api();

		public IActionResult Index()
		{
			var login = HttpContext.Session.GetString("Login_Success");
			if (login == "true")
				return View();

			return RedirectToAction("Index", "Login");
		}

		public IActionResult Detail(int id)
		{
			var tr_d = tr_api.GetAllTransaction().Result;
			var cart_d = cart_api.GetAllCart().Result;
			var pro_d = pro_api.GetAllProductFood().Result;
			var cur_d = cur_api.GetAllCustomers_cur().Result;
			string cus_noid = HttpContext.Session.GetString("UserIdNo");

			var tr_find = tr_d.Where(x => x.TransactionId == id).FirstOrDefault();
			var cur_find = cur_d.Where(x => x.CustomersIdNo == cus_noid).FirstOrDefault();

			List<Cart> cart_list = new List<Cart>();


			int count = 0;
			decimal d = 0;
			string[] str = tr_find.CartIdNo.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var j in str)
			{
				var cart_data = cart_d.Where(x => x.CardIdNo == j).FirstOrDefault();
				cart_list.Add(cart_data);
				var product_data = pro_d.Where(x => x.ProductFoodIdNo == cart_data.ProductFoodIdNo).FirstOrDefault();
				d += cart_data.Quantity * product_data.Price;
				count += 1;
			}

			var data = (from c in cart_list
						join pro in pro_d on c.ProductFoodIdNo equals pro.ProductFoodIdNo
						select new
						{
							price = pro.Price.ToString("0.00"),
							name = pro.ProductFoodName,
							qua = c.Quantity
						}).ToList();

			ViewBag.time = tr_find.Date;
			ViewBag.success = tr_find.Status == true ? "Success" : "Fail";
			ViewBag.tr_noid = tr_find.TransactionIdNo;
			ViewBag.total = d;
			ViewBag.curname = cur_find.Username;
			ViewBag.email = cur_find.Email;
			ViewBag.curnoid = cur_find.CustomersIdNo;
			ViewBag.count = count;
			ViewBag.data = data;


			// 名字  单价  分量

			return View();
		}
	}
}
