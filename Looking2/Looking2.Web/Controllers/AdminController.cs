using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Looking2.Web.DataAccess;
using MongoDB.Driver;
using Looking2.Web.Domain;
using Microsoft.AspNetCore.Http;
using Looking2.Web.ViewModels;
using AutoMapper;

namespace Looking2.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ICategoriesRepository categoryRepo;
        private IEventFormsRepo eventFormsRepo;
        private IBusinessFormsRepo businessFormsRepo;
        private IMapper mapper;

        public AdminController(ICategoriesRepository _categoryRepo, IEventFormsRepo _eventFormRepo, IBusinessFormsRepo _businessFormRepo, IMapper _mapper)
        {
            this.categoryRepo = _categoryRepo;
            this.eventFormsRepo = _eventFormRepo;
            this.businessFormsRepo = _businessFormRepo;
            this.mapper = _mapper;
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
        public IActionResult EditEventForm(string name)
        {
            var model = eventFormsRepo.GetByName(name+"Create");
            if (model == null)
            {
                throw new Exception("form not found");
            }
            model.Type = getEventTypeByName(name);
            var vm = Mapper.Map<EventFormData, EventFormViewModel>(model);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEventForm(EventFormViewModel vm)
        {
            var model = Mapper.Map<EventFormViewModel, EventFormData>(vm);
            model = eventFormsRepo.Update(model);
            ViewData["Message"] = string.Format("Successfully Updated: {0}", model.FormName);
            return View("Result");
        }

        [HttpGet]
        public IActionResult EditBusinessForm(string name)
        {
            var model = businessFormsRepo.GetByName(name+"Create");
            if (model == null)
            {
                throw new Exception("form not found");
            }
            model.BusinessType = getBusinessTypeByName(name);
            var vm = Mapper.Map<BusinessFormData, BusinessFormViewModel>(model);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBusinessForm(BusinessFormViewModel vm)
        {
            var model = Mapper.Map<BusinessFormViewModel, BusinessFormData>(vm);
            model = businessFormsRepo.Update(model);
            ViewData["Message"] = string.Format("Successfully Updated: {0}", model.FormName);
            return View("Result");
        }

        // TODO : Add details view for redirect after editing
        //public IActionResult EventFormDetails(string id)
        //{

        //}
        #region Helpers
        private BusinessType getBusinessTypeByName(string name)
        {
            BusinessType type;
            if (Enum.TryParse(name, out type))
            {
                return type;
            }
            else
            {
                return BusinessType.Support;
                //model.Listing.BusinessDescription = EventDescription.Other.ToString();
            }
        }

        private EventType getEventTypeByName(string name)
        {
            EventType type;
            if (Enum.TryParse(name, out type))
            {
                return type;
            }
            else
            {
                return EventType.Other;
            }
        }
        #endregion
    }
}