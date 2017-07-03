using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Looking2.Web.DataAccess;
using Looking2.Web.Domain;
using Looking2.Web.ViewModels;

namespace Looking2.Web.Controllers
{
    public class BusinessesController : Controller
    {
        private IBusinessRepository businessRepo;
        private ICategoriesRepository categoryRepo;

        public BusinessesController(IBusinessRepository _businessRepo, ICategoriesRepository _categoryRepo)
        {
            this.businessRepo = _businessRepo;
            this.categoryRepo = _categoryRepo;
        }

        public IActionResult Index()
        {
            var listings = businessRepo.GetAll();
            var viewListings = new List<BusinessDetailsViewModel>();
            foreach (var item in listings)
            {
                viewListings.Add(new BusinessDetailsViewModel(item));
            }
            return View(viewListings);
        }

        [HttpGet]
        public IActionResult CategoryIndex()
        {
            var eventCategories = categoryRepo.GetByType(CategoryType.Business);
            return View(eventCategories);
        }

        [HttpGet]
        public IActionResult Create(string businessType)
        {
            var model = new BusinessListing();
            BusinessType type;
            if (Enum.TryParse(businessType, out type))
            {
                switch (type)
                {
                    case BusinessType.Artists:
                        break;
                    case BusinessType.HealthCare:
                    case BusinessType.AltHealthCare:
                        model.BusinessCategory = BusinessCategory.Other;
                        break;
                    case BusinessType.Information:
                        break;
                    case BusinessType.Instruction:
                        break;
                    case BusinessType.Lawyers:
                        break;
                    case BusinessType.Restaurant:
                        break;
                    case BusinessType.ServiceProviders:
                        break;
                    case BusinessType.Shopkeepers:
                        break;
                    case BusinessType.Support:
                        break;
                    default:
                        break;
                }
                model.BusinessCategory = BusinessCategory.Other;
                model.BusinessType = type;
            }
            else
            {

            }

            //create empty fields for view
            model.Initialize();
            return View(businessType + "Create", model);
        }

        [HttpPost]
        public IActionResult Create(BusinessListing model)
        {
            model.Clean();
            businessRepo.Add(model);
            return RedirectToAction("Details", new { id = model.Id.ToString() });
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var listing = businessRepo.GetById(id);
            var vm = new BusinessDetailsViewModel(listing);
            return View(vm);
        }
    }
}