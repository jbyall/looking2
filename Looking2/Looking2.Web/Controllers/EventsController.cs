using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Looking2.Web.ViewModels;

namespace Looking2.Web.Controllers
{
    public class EventsController : Controller
    {
        #region Fundraisers
        [HttpGet]
        public ActionResult CreateFundraisers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFundraisers(FundraiserViewModel model)
        {
            return RedirectToAction("DetailsFundraisers");
        }

        public ActionResult DetailsFundraisers()
        {
            return View();
        }
        #endregion

        #region Gigs
        [HttpGet]
        public ActionResult CreateGigs()
        {
            var model = new GigViewModel();
            return View(model);
        }
        #endregion

        // GET: Events
        public ActionResult Index()
        {
            return View();
        }

        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Events/Edit/5
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

        // GET: Events/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Events/Delete/5
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