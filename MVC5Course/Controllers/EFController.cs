using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
	public class EFController : Controller
	{
		FabricsEntities db = new FabricsEntities();
		// GET: EF
		public ActionResult Index(bool? IsActive, string keyword)
		{
			//Object Service

			var product = new Product()
			{
				ProductName = "BMW",
				Price = 2,
				Stock = 1,
				Active = true
			};
			db.Product.Add(product);
			SaveChanges();
			var pkey = product.ProductId;

			var data = db.Product.OrderByDescending(p => p.ProductId).AsQueryable();
			if (IsActive.HasValue)
			{
				data = data.Where(p => p.Active.HasValue ? p.Active.Value == IsActive.Value : false);
			}

			if (!String.IsNullOrEmpty(keyword))
			{
				data = data.Where(p => p.ProductName.Contains(keyword));
			}

			//Execute T-SQL in DB
			//db.Database.ExecuteSqlCommand("");

			SaveChanges();
			return View(data);
		}

		private void SaveChanges()
		{
			try
			{
				db.SaveChanges();
			}
			catch (DbEntityValidationException ex)
			{
				var allErrors = new List<string>();
				foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
				{
					foreach (DbValidationError re in item.ValidationErrors)
					{
						//err.ErrorMessage
						string entityName = item.Entry.Entity.GetType().Name;
						foreach (DbValidationError err in item.ValidationErrors)
						{
							throw new Exception(entityName + "資料驗證失敗:" + err.ErrorMessage);
						}
					}
				}

			}
		}

		public ActionResult Detail(int id)
		{
			var data = db.Product.FirstOrDefault(p => p.ProductId == id);
			return View(data);
		}

		public ActionResult Delete(int id)
		{
			var product = db.Product.Find(id);
			//單筆移除
			//db.Product.Remove(item);
			//多筆移除
			db.OrderLine.RemoveRange(product.OrderLine);
			db.Product.Remove(product);
			SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult QueryPlan(int num = 10)
		{
			var data = db.Product.
				Include(t => t.OrderLine)
				.OrderBy(p => p.ProductId);
			//強迫轉型,所以QueryPlan OrderLine Count值為0
			//var data = db.Database.SqlQuery<Product>(@"
			//SELECT *
			//FROM dbo.Product p
			//	 JOIN dbo.OrderLine o ON (p.ProductId = o.ProductId)
			//             WHERE
			//              p.ProductId <@p0
			//",num);

			return View(data);
		}
	}
}