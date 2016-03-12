using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
			//參數為字串, ie.Index2
			return View();
        }

		public ActionResult Index2()
		{
			//參數為字串, ie.Index2
			return PartialView("Index");
		}
		//可以少寫一個View 不建議使用
	    public ActionResult ContentTest()
	    {
			return Content("<script>alert('Redricting...');</script>");
	    }

		public ActionResult FileTest()
		{
			//MINE TYPE沒有設對 變成下載
			return File(Server.MapPath("~/Content/下載.png"),
				"image/png","GoGOGO.png");
			//下載檔案,檔名為"GoGOGO.png"
		}

		public JsonResult JsonTest()
		{
			var db = new FabricsEntities();
			db.Configuration.LazyLoadingEnabled = false;
			var data = db.Product.Take(3);
			return Json(data,JsonRequestBehavior.AllowGet);
		}
	}
}