using SniperdyneDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Data.Entity;

namespace SniperdyneDemo.Controllers
{
	//  [Authorize]
	public class HomeController : Controller
	{
		private MyDBEntities db = new MyDBEntities();

		[HttpGet]
		[AllowAnonymous]
		public ActionResult Index(string searchBy, string search, int? page)
		{
			try
			{
				if (searchBy == "VehiclesName")
				{
					if (search == "")
					{
						return View(db.Vehicles.ToList().OrderBy(x => x.VehiclesID).ToPagedList(page ?? 1, 3));
					}
					return View(db.Vehicles.Where(x => x.VehiclesName == search || search == null).ToList()
					.OrderBy(x => x.VehiclesID).ToPagedList(page ?? 1, 3));
				}
				return View(db.Vehicles.ToList().OrderBy(x => x.VehiclesID).ToPagedList(page ?? 1, 3));
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return null;
			}
		}

		[AllowAnonymous]
		[HttpGet]
		public ActionResult Create()
		{
			try
			{
				Vehicles vehicle = new Vehicles();
				return View(vehicle);
			}
			catch (Exception ex )
			{
				string message = ex.Message;
				return null;
			}
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult Create(Vehicles model, HttpPostedFileBase image1)
		{
			try
			{
				var db = new MyDBEntities();
				if (image1 != null)
				{
					model.VehiclessImages = new byte[image1.ContentLength];
					image1.InputStream.Read(model.VehiclessImages, 0, image1.ContentLength);
				}
				db.Vehicles.Add(model);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				string exc = ex.Message;
				return null;
			}
		}

		[AllowAnonymous]
		// GET: Home/Details/5
		public ActionResult Details(int id)
		{
			try
			{

				return View();
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return null;
			}
		}

		// GET: Home/Edit/5
		[HttpGet]
		[AllowAnonymous]
		public ActionResult Edit(int id)
		{
			try
			{
				Vehicles vehicle = db.Vehicles.Single(v => v.VehiclesID == id);
				return View(vehicle);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return null;
			}
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult Edit(Vehicles model, HttpPostedFileBase image1)
		{
			try
			{
				if (image1 != null)
				{
					model.VehiclessImages = new byte[image1.ContentLength];
					image1.InputStream.Read(model.VehiclessImages, 0, image1.ContentLength);
				}
				db.Entry(model).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return null;
			}

		}

		// GET: Home/Delete/5
		[AllowAnonymous]
		public ActionResult Delete(int id)
		{
			try
			{
				return View();
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return null;
			}
		}

		// POST: Home/Delete/5
		[AllowAnonymous]
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				var itemToRemove = db.Vehicles.SingleOrDefault(x => x.VehiclesID == id); //returns a single item.
				if (itemToRemove != null)
				{
					db.Vehicles.Remove(itemToRemove);
					db.SaveChanges();
				}
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return null;
			}
		}
	}
}