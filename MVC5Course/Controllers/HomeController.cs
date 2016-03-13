using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.ActionFilters;

namespace MVC5Course.Controllers
{
	
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
		[紀錄Action的執行時間]
		//[共用的ViewBag資料共享於部分HomeController動作方法]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

		public ActionResult Test()
		{
			return View();
		}

		[HandleError(ExceptionType = typeof(ArgumentException), View = "ErrorArgument")]
        [HandleError(ExceptionType = typeof(SqlException), View = "ErrorSql")]
		public ActionResult ErrorTest(string e)
		{
			if(e=="1")
			{
				throw new Exception("Error 1");
			}

			if (e == "2")
			{
				throw new Exception("Error 2");
			}
			return Content("No Error");
		}

		public ActionResult RazerTest()
		{
			Int32[] data =new Int32[] { 1, 2, 3, 4, 5 };
			return PartialView(data);
		}
    }
}