using System;
using System.Net.Http.Headers;
using Fried_Rice_DomainModelEntity.Models;
using Fried_Rice_Insfrastructure.Repository.Api;
using Fried_Rice_Insfrastructure.Repository.Token;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace Fried_Rice.Controllers
{
	public class LoginController : Controller
	{
		public Customers_Api cus_api = new Customers_Api();
		public static bool login = false;

		public IActionResult Index()
		{
			var infor = HttpContext.Session.GetString("InformationLogin");
			var infor_s = HttpContext.Session.GetString("SuccessInformationLogin");
			if (infor != null)
			{
				if (infor != "")
				{
					//注册失败
					ViewBag.user = infor;
					ViewBag.tru = false;
				}
				else if (infor_s != "")
				{
					//注册成功
					ViewBag.user = infor_s;
					ViewBag.tru = true;
				}
			}

			HttpContext.Session.SetString("InformationLogin", "");
			HttpContext.Session.SetString("SuccessInformationLogin", "");
			return View();
		}

		public IActionResult OutLogin()
		{
			HttpContext.Session.SetString("Login_Success", "false");
			login = false;
			return RedirectToAction("Home", "Home");
		}

		[HttpPost]
		public IActionResult Login(Customers cur)
		{
			var db = cus_api.GetAllCustomers().Result;
			var data = db.Where(x => x.Username == cur.Username && x.Password == cur.Password).FirstOrDefault();
			if (data != null)
			{
				HttpContext.Session.SetString("UserIdNo", data.CustomersIdNo);
				HttpContext.Session.SetString("Login_Success", "true");
				login = true;
				CustomerToken.Userid_Token = data.Username;
				CustomerToken.Password_Token = data.Password;
				return RedirectToAction("Home", "Home");
			}

			HttpContext.Session.SetString("InformationLogin", "登入失败");
			return RedirectToAction("Index", "Login");
		}

		[HttpPost]
		public IActionResult SignUp(Customers cur)
		{
			var db = cus_api.GetAllCustomers().Result;
			var checkData_user = db.Where(x => x.Username == cur.Username).FirstOrDefault();
			var checkData_pass = db.Where(x => x.Password == cur.Password).FirstOrDefault();
			var checkData_email = db.Where(x => x.Email == cur.Email).FirstOrDefault();
			if (checkData_user != null)
			{
				HttpContext.Session.SetString("InformationLogin", "请使用别的ID");
				return RedirectToAction("Index", "Login");
			}
			else if (checkData_pass != null)
			{
				HttpContext.Session.SetString("InformationLogin", "请使用别的密码");
				return RedirectToAction("Index", "Login");
			}
			else if (checkData_email != null)
			{
				HttpContext.Session.SetString("InformationLogin", "请使用别的邮件");
				return RedirectToAction("Index", "Login");
			}


			string New_curNoID = null;
			if (db.Count != 0)
			{
				string Old_curNoID = db.LastOrDefault().CustomersIdNo;
				int num = Convert.ToInt32(Old_curNoID.Substring(4)) + 1;
				New_curNoID = "Cur-" + num;
			}
			else
				New_curNoID = "Cur-1";

			Customers curs = new Customers()
			{
				CustomersIdNo = New_curNoID,
				CustomersName = "美食者",
				Email = cur.Email,
				Avatar = "默认Ava.jpeg",
				Username = cur.Username,
				Password = cur.Password,
				Active = true
			};

			cus_api.CustomerCreateData(curs);
			HttpContext.Session.SetString("SuccessInformationLogin", "注册成功");
			return RedirectToAction("Index", "Login");
		}
	}
}
