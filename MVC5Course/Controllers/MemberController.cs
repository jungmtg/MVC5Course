using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
	[Authorize]
    public class MemberController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
		[HttpPost]
		[AllowAnonymous]
		public ActionResult Login(LoginViewModel login)
		{
			if (CheckLogin(login.Email, login.Password))
			{
				FormsAuthentication.RedirectFromLoginPage(login.Email,false);
				return RedirectToAction("Index","Home");
			}
			ModelState.AddModelError("Password","你登入的帳號或密碼錯誤");
			return View();
		}
		[HttpPost]
		public ActionResult Logout()
		{
			return RedirectToAction("Index", "Home");
		}
		private bool CheckLogin(string email, string password)
		{
			return (email=="jungmtg@gmail.com" && password=="iec123")
			;
		}
    }
}