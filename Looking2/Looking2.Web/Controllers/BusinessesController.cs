using System;
using System.Collections.Generic;
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
        private IBusinessFormsRepo formsRepo;

        public BusinessesController(IBusinessRepository _businessRepo, ICategoriesRepository _categoryRepo, IBusinessFormsRepo _formsRepo)
        {
            this.businessRepo = _businessRepo;
            this.categoryRepo = _categoryRepo;
            this.formsRepo = _formsRepo;
        }

        [HttpGet]
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
            var eventCategories = categoryRepo.GetByType(ListingCategory.Business);
            return View(eventCategories);
        }

        [HttpGet]
        public IActionResult Create(string businessType)
        {
            var model = getModelByBusinessType(businessType);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(BusinessListingViewModel model)
        {
            model.Listing.Clean();
            businessRepo.Add(model.Listing);
            return RedirectToAction("Details", new { id = model.Listing.Id.ToString() });
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var listing = businessRepo.GetById(id);
            var vm = new BusinessDetailsViewModel(listing);
            return View(vm);
        }

        #region Helpers
        private BusinessListingViewModel getModelByBusinessType(string businessType)
        {
            var model = new BusinessListingViewModel();
            BusinessType type;
            if (Enum.TryParse(businessType, out type))
            {
                switch (type)
                {
                    case BusinessType.Artists:
                        model.FormData = formsRepo.GetByName("ArtistsCreate");
                        //model.Listing.BusinessDescription = EventDescription.Other.ToString();
                        break;
                    case BusinessType.HealthCare:
                        model.FormData = formsRepo.GetByName("HealthCareCreate");
                        //model.Listing.BusinessDescription = EventDescription.Other.ToString();
                        break;
                    case BusinessType.AltHealthCare:
                        model.FormData = formsRepo.GetByName("AltHealthCareCreate");
                        //model.Listing.BusinessDescription = EventDescription.Other.ToString();
                        break;
                    case BusinessType.Information:
                        model.FormData = formsRepo.GetByName("InformationCreate");
                        //model.Listing.BusinessDescription = EventDescription.Other.ToString();
                        break;
                    case BusinessType.Instruction:
                        model.FormData = formsRepo.GetByName("InstructionCreate");
                        //model.Listing.BusinessDescription = EventDescription.Other.ToString();
                        break;
                    case BusinessType.Lawyers:
                        model.FormData = formsRepo.GetByName("LawyersCreate");
                        //model.Listing.BusinessDescription = EventDescription.Other.ToString();
                        break;
                    case BusinessType.Restaurant:
                        model.FormData = formsRepo.GetByName("RestaurantCreate");
                        //model.Listing.BusinessDescription = EventDescription.Other.ToString();
                        break;
                    case BusinessType.ServiceProviders:
                        model.FormData = formsRepo.GetByName("ServiceProvidersCreate");
                        //model.Listing.BusinessDescription = EventDescription.Other.ToString();
                        break;
                    case BusinessType.Shopkeepers:
                        model.FormData = formsRepo.GetByName("ShopkeepersCreate");
                        //model.Listing.BusinessDescription = EventDescription.Other.ToString();
                        break;
                    case BusinessType.Support:
                        model.FormData = formsRepo.GetByName("SupportCreate");
                        //model.Listing.BusinessDescription = EventDescription.Other.ToString();
                        break;
                    default:
                        break;
                }
                //model.Listing.BusinessDescription = EventDescription.Other.ToString();
                model.Listing.BusinessType = type;
            }
            else
            {
                model.FormData = formsRepo.GetByName("OtherCreate");
                //model.Listing.BusinessDescription = EventDescription.Other.ToString();
            }

            //create empty fields for view
            model.Listing.Initialize();

            return model;
        }
        #endregion
    }
}