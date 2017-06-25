using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Looking2.Web.Domain;
using Looking2.Web.DataAccess;

namespace Looking2.Web.Controllers
{
    public class CatsController : Controller
    {
        // GET: Cats
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cats/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cats/Create
        public ActionResult Create()
        {
            return View(new Cat());
        }

        // POST: Cats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cat model)
        {
            
                // TODO: Add insert logic here
                Repository repo = new Repository();
                repo.InsertCat(model);
                return RedirectToAction("Index");
            
        }

        // GET: Cats/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cats/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cats/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}