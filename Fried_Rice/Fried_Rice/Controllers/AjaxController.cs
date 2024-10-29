using System.Data;
using Fried_Rice_DomainModelEntity.Models;
using Fried_Rice_Insfrastructure.Repository.Api;
using Microsoft.AspNetCore.Mvc;

namespace Fried_Rice.Controllers
{
	public class AjaxController : Controller
	{
		public ProductFood_Api pro_api = new ProductFood_Api();
		public Cart_Api cart_api = new Cart_Api();
		public Customers_Api cur_api = new Customers_Api();
		public Transaction_Api tr_api = new Transaction_Api();

		public IActionResult GetMore(string type)
		{
			var db = pro_api.GetAllProductFood().Result;

			if (type == "getMore")
			{
				return Json(db);
			}
			else if (type == "xiaochi")
			{
				var data = db.Where(x => x.Type.Contains("小吃")).ToList();
				return Json(data);
			}
			else if (type == "sweet")
			{
				var data = db.Where(x => x.Type.Contains("甜点")).ToList();
				return Json(data);
			}
			else if (type == "breah")
			{
				var data = db.Where(x => x.Type.Contains("面包")).ToList();
				return Json(data);
			}
			else if (type == "homefood")
			{
				var data = db.Where(x => x.Type.Contains("家常菜")).ToList();
				return Json(data);
			}
			else if (type == "炒饭")
			{
				var data = db.Where(x => x.Type.Contains("炒饭")).ToList();
				return Json(data);
			}
			else if (type == "面")
			{
				var data = db.Where(x => x.Type.Contains("面")).ToList();
				return Json(data);
			}
			else if (type == "披萨")
			{
				var data = db.Where(x => x.Type.Contains("披萨")).ToList();
				return Json(data);
			}
			else if (type == "饭")
			{
				var data = db.Where(x => x.Type.Contains("饭")).ToList();
				return Json(data);
			}
			else if (type == "鸡肉")
			{
				var data = db.Where(x => x.Type.Contains("鸡肉")).ToList();
				return Json(data);
			}

			return Json(false);

		}

		public IActionResult GetCart()
		{
			var db = pro_api.GetAllProductFood().Result;
			var data = cart_api.GetAllCart().Result;
			string cus_noid = HttpContext.Session.GetString("UserIdNo");

			var list = (from c in data
						where c.CustomersIdNo == cus_noid && c.Status == false
						join p in db on c.ProductFoodIdNo equals p.ProductFoodIdNo
						select new
						{
							p_img = p.FoodImg,
							p_name = p.ProductFoodName,
							p_price = p.Price,
							p_type = p.Type,
							c_qua = c.Quantity,
							c_id = c.CartId
						}).ToList();

			return Json(list);
		}

		public IActionResult GetCart_plus(int cart_id)
		{
			var db = pro_api.GetAllProductFood().Result;
			var data = cart_api.GetAllCart().Result;
			string cus_noid = HttpContext.Session.GetString("UserIdNo");

			foreach (var i in data)
			{
				if (i.CartId == cart_id)
				{
					i.Quantity += 1;
					cart_api.EditCart(i);
				}
			}

			var data2 = cart_api.GetAllCart().Result;
			var list = (from c in data2
						where c.CustomersIdNo == cus_noid && c.Status == false
						join p in db on c.ProductFoodIdNo equals p.ProductFoodIdNo
						select new
						{
							p_img = p.FoodImg,
							p_name = p.ProductFoodName,
							p_price = p.Price,
							p_type = p.Type,
							c_qua = c.Quantity,
							c_id = c.CartId
						}).ToList();

			return Json(list);
		}

		public IActionResult GetCart_minus(int cart_id)
		{
			var db = pro_api.GetAllProductFood().Result;
			var data = cart_api.GetAllCart().Result;
			string cus_noid = HttpContext.Session.GetString("UserIdNo");

			foreach (var i in data)
			{
				if (i.CartId == cart_id)
				{

					if ((i.Quantity - 1) <= 0)
					{
						cart_api.DeleteCustomer(cart_id);
					}
					else
					{
						i.Quantity -= 1;
						cart_api.EditCart(i);
					}
				}
			}

			var data2 = cart_api.GetAllCart().Result;
			var list = (from c in data2
						where c.CustomersIdNo == cus_noid && c.Status == false
						join p in db on c.ProductFoodIdNo equals p.ProductFoodIdNo
						select new
						{
							p_img = p.FoodImg,
							p_name = p.ProductFoodName,
							p_price = p.Price,
							p_type = p.Type,
							c_qua = c.Quantity,
							c_id = c.CartId
						}).ToList();

			return Json(list);
		}

		public IActionResult GetTotal()
		{
			var db = pro_api.GetAllProductFood().Result;
			var data = cart_api.GetAllCart().Result;
			string cus_noid = HttpContext.Session.GetString("UserIdNo");
			decimal total = 0;
			string date = DateTime.Now.ToString("d");
			int count = data.Where(x => x.CustomersIdNo == cus_noid && x.Status == false).Count();

			foreach (var i in data)
			{
				if (i.CustomersIdNo == cus_noid && i.Status == false)
				{
					foreach (var j in db)
					{
						if (j.ProductFoodIdNo == i.ProductFoodIdNo)
						{
							total += j.Price * i.Quantity;
						}
					}
				}
			}

			var list = (from c in data
						select new
						{
							date = date,
							total = total,
							count = count
						}).FirstOrDefault();

			return Json(list);
		}

		public IActionResult Buy(string gmail, decimal total)
		{
			var db = pro_api.GetAllProductFood().Result;
			var data = cart_api.GetAllCart().Result;
			string cus_noid = HttpContext.Session.GetString("UserIdNo");
			var cur_data = cur_api.GetAllCustomers_cur().Result;
			var check = data.Where(x => x.CustomersIdNo == cus_noid && x.Status == false).Count();
			if (check <= 0)
				return Json("false");

			Customers cur = new Customers();
			foreach (var i in cur_data)
			{
				if (i.CustomersIdNo == cus_noid)
					cur = i;
			}

			if (cur.Email != gmail)
				return Json("email");

			string cartList = null;
			foreach (var i in data)
			{
				if (i.Status == false && i.CustomersIdNo == cus_noid)
				{
					cartList += $"{i.CardIdNo}|";
					i.Status = true;
					cart_api.EditCart(i);
				}

			}

			var tr_data = tr_api.GetAllTransaction().Result;
			string idNo = null;
			if (tr_data.Count > 0)
			{
				string CartNoid_old = tr_data.LastOrDefault().TransactionIdNo;
				int Noid_new = Convert.ToInt32(CartNoid_old.Substring(3)) + 1;
				idNo = "Tr-" + Noid_new;
			}
			else
			{
				idNo = "Tr-1";
			}

			Transaction tr = new Transaction()
			{
				TransactionIdNo = idNo,
				CustomersIdNo = cus_noid,
				CartIdNo = cartList,
				Date = DateTime.Now.ToString("F"),
				Status = true
			};

			tr_api.TransactionCreate(tr);
			return Json(true);
		}

		public IActionResult GetTr()
		{
			var db = tr_api.GetAllTransaction().Result;
			var cart = cart_api.GetAllCart().Result;
			var product = pro_api.GetAllProductFood().Result;
			string cus_noid = HttpContext.Session.GetString("UserIdNo");
			var list = db.Where(x => x.CustomersIdNo == cus_noid).ToList();

			List<decimal> total = new List<decimal>();
			foreach (var i in list)
			{
				decimal d = 0;
				string[] str = i.CartIdNo.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (var j in str)
				{
					var cart_data = cart.Where(x => x.CardIdNo == j).FirstOrDefault();
					var product_data = product.Where(x => x.ProductFoodIdNo == cart_data.ProductFoodIdNo).FirstOrDefault();
					d += cart_data.Quantity * product_data.Price;

				}
				total.Add(d);
			}

			var data = (from c in list
						select new
						{
							id = c.TransactionId,
							time = c.Date,
							noid = c.TransactionIdNo,
							status = c.Status == true ? "成功" : "失败",
							price = total
						}).OrderByDescending(x => x.id).ToList();

			return Json(data);
		}

	}
}
