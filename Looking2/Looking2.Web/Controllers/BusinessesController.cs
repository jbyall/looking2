using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Looking2.Web.DataAccess;
using Looking2.Web.Domain;
using Looking2.Web.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Looking2.Web.Settings;
using MongoDB.Driver;

namespace Looking2.Web.Controllers
{
    public class BusinessesController : Controller
    {
        private IBusinessRepository businessRepo;
        private ICategoriesRepository categoryRepo;
        private IConfiguration configuration;
        private ConnectionStrings connectionStrings;

        public BusinessesController(IBusinessRepository _businessRepo, ICategoriesRepository _categoryRepo, IConfiguration _configuration, IOptions<ConnectionStrings> _connectionStrings)
        {
            this.businessRepo = _businessRepo;
            this.categoryRepo = _categoryRepo;
            this.configuration = _configuration;
            this.connectionStrings = _connectionStrings.Value;
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
            var db = new Looking2DbContext(connectionStrings.Looking2DbConnection);
            var model = new BusinessListingViewModel();
            BusinessType type;
            if (Enum.TryParse(businessType, out type))
            {
                switch (type)
                {
                    case BusinessType.Artists:
                        model.FieldSet = db.BusinessForms.Find(f => f.FormName == "ArtistsCreate").SingleOrDefault();
                        model.Listing.BusinessCategory = BusinessCategory.Other;
                        break;
                    case BusinessType.HealthCare:
                        model.FieldSet = db.BusinessForms.Find(f => f.FormName == "HealthCareCreate").SingleOrDefault();
                        model.Listing.BusinessCategory = BusinessCategory.Other;
                        break;
                    case BusinessType.AltHealthCare:
                        model.FieldSet = db.BusinessForms.Find(f => f.FormName == "AltHealthCareCreate").SingleOrDefault();
                        model.Listing.BusinessCategory = BusinessCategory.Other;
                        break;
                    case BusinessType.Information:
                        model.FieldSet = db.BusinessForms.Find(f => f.FormName == "InformationCreate").SingleOrDefault();
                        model.Listing.BusinessCategory = BusinessCategory.Other;
                        break;
                    case BusinessType.Instruction:
                        model.FieldSet = db.BusinessForms.Find(f => f.FormName == "InstructionCreate").SingleOrDefault();
                        model.Listing.BusinessCategory = BusinessCategory.Other;
                        break;
                    case BusinessType.Lawyers:
                        model.FieldSet = db.BusinessForms.Find(f => f.FormName == "LawyersCreate").SingleOrDefault();
                        model.Listing.BusinessCategory = BusinessCategory.Other;
                        break;
                    case BusinessType.Restaurant:
                        model.FieldSet = db.BusinessForms.Find(f => f.FormName == "RestaurantCreate").SingleOrDefault();
                        model.Listing.BusinessCategory = BusinessCategory.Other;
                        break;
                    case BusinessType.ServiceProviders:
                        model.FieldSet = db.BusinessForms.Find(f => f.FormName == "ServiceProvidersCreate").SingleOrDefault();
                        model.Listing.BusinessCategory = BusinessCategory.Other;
                        break;
                    case BusinessType.Shopkeepers:
                        model.FieldSet = db.BusinessForms.Find(f => f.FormName == "ShopkeepersCreate").SingleOrDefault();
                        model.Listing.BusinessCategory = BusinessCategory.Other;
                        break;
                    case BusinessType.Support:
                        model.FieldSet = db.BusinessForms.Find(f => f.FormName == "SupportCreate").SingleOrDefault();
                        model.Listing.BusinessCategory = BusinessCategory.Other;
                        break;
                    default:
                        break;
                }
                model.Listing.BusinessCategory = BusinessCategory.Other;
                model.Listing.BusinessType = type;
            }
            else
            {
                model.FieldSet = db.BusinessForms.Find(f => f.FormName == "OtherCreate").SingleOrDefault();
                model.Listing.BusinessCategory = BusinessCategory.Other;
            }

            //create empty fields for view
            model.Listing.Initialize();
            return View(model);
            //return View(businessType + "Create", model);
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