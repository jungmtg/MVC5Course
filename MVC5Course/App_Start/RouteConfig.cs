using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5Course
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

			//Home/About
			//{resource}.axd 路由變數 到分隔線結束
			//
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//Controller = Home
			//Action = About
			//id = nullable
			//http://localhost:10681/Home/About/1/1 IIS404 Error
			//http://localhost:10681/Home/About2/2
			//url 不會有QueryString 也不會有任何比對

			routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home",
								action = "Index",
								id = UrlParameter.Optional }
				//,
				//constraints:new
				//{
				//	id=@"\d"
				//}

            );
			//UrlParameter.Optional 空物件
		}
	}
}
