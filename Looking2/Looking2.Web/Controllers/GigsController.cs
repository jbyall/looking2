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
    public class GigsController : Controller
    {
        private Repository repo;
        public GigsController()
        {
            this.repo = new Repository();
        }
        // GET: Gigs
        public ActionResult Index()
        {
            var allGigs = repo.Gigs();
            return View(allGigs);
        }

        // GET: Gigs/Details/5
        public ActionResult Details(string id)
        {
            var model = repo.FindGig(id);
            return View(model);
        }

        // GET: Gigs/Create
        public ActionResult Create()
        {
            var gig = new Gig();
            return View(gig);
        }

        // POST: Gigs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gig model)
        {
            var gig = repo.InsertGig(model);
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Details", new { id = gig.Id.ToString()});
            }
            catch
            {
                return View();
            }
        }

        // GET: Gigs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Gigs/Edit/5
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

        // GET: Gigs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Gigs/Delete/5
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