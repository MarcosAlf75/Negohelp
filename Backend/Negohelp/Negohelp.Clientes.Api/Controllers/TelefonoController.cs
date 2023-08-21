using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Negohelp.Clientes.Api.Controllers
{
	public class TelefonoController : Controller
	{
		// GET: TelefonoController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: TelefonoController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: TelefonoController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: TelefonoController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: TelefonoController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: TelefonoController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: TelefonoController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
