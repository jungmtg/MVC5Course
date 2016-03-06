using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
		//Code Snippet shortcut:mvcaction4
	    public ActionResult MemberProfile()
	    {
		    return View();
	    }
		//mvcpostaction4
		[HttpPost]
		public ActionResult MemberProfile(MemberViewModel data)
		{
			return View();
		}
		
    }
}