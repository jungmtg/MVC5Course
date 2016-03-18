using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
	public class ProductsController : BaseController//Controller
	{
		//private FabricsEntities db = new FabricsEntities();
		private ProductRepository repo = RepositoryHelper.GetProductRepository();
		// GET: Products
		public ActionResult Index(int? ProductId, string type)
		{
			var data = repo.All().Take(5);
			//var repoOL = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork);
			//repo.Get超級複雜的資料集();
			ViewBag.type = type;

			if (ProductId.HasValue)
			{
				ViewBag.SelectedProductId = ProductId.Value;
			}
			//String Type 強型別
			//ViewData.Model = data;
			//return View(ViewData.Model);
			return View(data);
		}

		[HttpPost]
		//public ActionResult Index(IList<Product> data)
		public ActionResult Index(IList<Products批次更新ViewModel> data)
		{
			//foreach (var item in products)
			//{
			//	var product = repo.Find(item.ProductId);
			//	product.Stock = item.Stock;
			//	product.Price = item.Price;
			//}

			if (ModelState.IsValid)
			{
				foreach (var item in data)
				{
					var product = repo.Find(item.ProductId);
					product.Stock = item.Stock;
					product.Price = item.Price;
				}

				repo.UnitOfWork.Commit();
				return RedirectToAction("Index");
			}

			return View(repo.All().Take(5));


		}



		// GET: Products/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = repo.Find(id.Value);

			if (product == null && product.IsDeleted)
			{
				return HttpNotFound();
			}


			ViewBag.OrderLines = product.OrderLine.ToList();

			return View(product);
		}



		// GET: Products/Create
		public ActionResult Create()
		{
			//只顯示Create表單
			return View();
		}

		// POST: Products/Create
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
		//Product product 強型別
		{
			if (ModelState.IsValid)
			{
				var db = (FabricsEntities)repo.UnitOfWork.Context;
				db.Product.Add(product);
				db.SaveChanges();
				TempData["ProductEditDoneMeg"] = "商品編輯成功";
				return RedirectToAction("Index");
			}

			return View(product);
		}

		// GET: Products/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = repo.Find(id.Value);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// POST: Products/Edit/5
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)

		//{

		//	if (ModelState.IsValid)
		//	{
		//		var db = (FabricsEntities)repo.UnitOfWork.Context;
		//		db.Entry(product).State = EntityState.Modified;
		//		db.SaveChanges();
		//		TempData["ProductsEditDoneMsg"] = "商品編輯成功";
		//		return RedirectToAction("Index");
		//	}
		//	return View(product);
		//}

		[HttpPost]
		[ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
		public ActionResult Edit(int id, FormCollection form)
		{
			Product product = repo.Find(id);

			if (TryUpdateModel<Product>(product, new String[]
			{
				"ProductId","ProductName","Price","Active","Stock"
			}))
			{
				repo.UnitOfWork.Commit();
				TempData["ProductsEditDoneMsg"] = "商品編輯成功";
				return RedirectToAction("Index");
			}
			return View(product);
			//if (ModelState.IsValid)
			//{
			//	var db = (FabricsEntities)repo.UnitOfWork.Context;
			//	db.Entry(product).State = EntityState.Modified;
			//	db.SaveChanges();
			//	TempData["ProductsEditDoneMsg"] = "商品編輯成功";
			//	return RedirectToAction("Index");
			//}
			//return View(product);
		}

		// GET: Products/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = repo.Find(id.Value);
			if (product == null)
			{
				return HttpNotFound();
			}
			ViewBag.OrderLines = product.OrderLine.ToList();
			return View(product);
		}

		// POST: Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Product product = repo.Find(id);
			product.IsDeleted = true;
			repo.UnitOfWork.Commit();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				var db = (FabricsEntities)repo.UnitOfWork.Context;
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
