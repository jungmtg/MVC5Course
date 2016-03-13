using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.ActionFilters
{
	public class 紀錄Action的執行時間Attribute:ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			TimeSpan exectuionTime = TimeSpan.FromHours(1);
			filterContext.Controller.ViewBag.執行時間 = "執行時間="+exectuionTime;
			base.OnActionExecuted(filterContext);
		}

		//public override void OnActionExecuted(ActionExecutedContext filterContext)
		//{
		//	// 紀錄結束時間

		//	// 計算執行時間
		//	TimeSpan exectuionTime = TimeSpan.FromHours(1);

		//	filterContext.Controller.ViewBag.執行時間 = exectuionTime;

		//	base.OnActionExecuted(filterContext);
		//}

		//public override void OnActionExecuting(ActionExecutingContext filterContext)
		//{
		//	// 紀錄開始時間

		//	base.OnActionExecuting(filterContext);
		//}
	}
}