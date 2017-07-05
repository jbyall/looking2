using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Looking2.Web.DataAccess;
using MongoDB.Driver;
using Looking2.Web.Domain;

namespace Looking2.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ICategoriesRepository categoryRepo;
        private IEventFormsRepo eventFormsRepo;
        private IBusinessFormsRepo businessFormsRepo;
        public AdminController(ICategoriesRepository _categoryRepo, IEventFormsRepo _eventFormRepo, IBusinessFormsRepo _businessFormRepo)
        {
            this.categoryRepo = _categoryRepo;
            this.eventFormsRepo = _eventFormRepo;
            this.businessFormsRepo = _businessFormRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FormIndex()
        {
            var allCategories = categoryRepo.GetAll();
            return View(allCategories);
        }

        [HttpGet]
        public IActionResult EditEventForm(string id)
        {
            var model = eventFormsRepo.GetByName(id+"Create");
            if (model == null)
            {
                throw new Exception("form not found");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEventForm(EventFormData model)
        {
            model = eventFormsRepo.Update(model);
            return Content(string.Format("Successfully Updated: {0}", model.FormName));
        }

        [HttpGet]
        public IActionResult EditBusinessForm(string id)
        {
            var model = businessFormsRepo.GetByName(id+"Create");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBusinessForm(BusinessFormData model)
        {
            model = businessFormsRepo.Update(model);
            return Content(string.Format("Successfully Updated: {0}", model.FormName));
        }

    }
}