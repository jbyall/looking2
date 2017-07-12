using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Looking2.Web.Settings;
using Looking2.Web.DataAccess;

namespace Looking2.Web.Controllers
{
    [Authorize(Roles = "bond")]
    public class SeedController : Controller
    {
        private IOptions<DbSettings> settings;
        private ICategoriesRepository categoriesRepo;
        private IEventFormsRepo eventForms;
        private IBusinessFormsRepo businessForms;
        private IEventsRepository eventRepo;
        private IBusinessRepository businessRepo;

        public SeedController(IOptions<DbSettings> _settings, 
                                ICategoriesRepository _categoriesRepo,
                                IBusinessFormsRepo _businessForms,
                                IEventFormsRepo _eventForms,
                                IEventsRepository _eventRepo,
                                IBusinessRepository _businessRepo)
        {
            this.settings = _settings;
            this.categoriesRepo = _categoriesRepo;
            this.businessForms = _businessForms;
            this.eventForms = _eventForms;
            this.eventRepo = _eventRepo;
            this.businessRepo = _businessRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SeedCategories()
        {
            Seed.SeedCategories(categoriesRepo);
            return Content("Categories seeded successfully");
        }

        public IActionResult SeedForms()
        {
            Seed.SeedForms(businessForms, eventForms);
            return Content("Forms seeded successfully");
        }

        public IActionResult SeedBusinesses()
        {
            Seed.SeedBusinesses(businessRepo);
            return Content("Businesses seeded successfully");
        }

        public IActionResult SeedEvents()
        {
            Seed.SeedEvents(eventRepo);
            return Content("Events seeded successfully");
        }

        public IActionResult SeedAll()
        {
            Seed.SeedAll(categoriesRepo, businessForms, eventForms, eventRepo, businessRepo);
            return Content("All done.");
        }

        public IActionResult DropListingCollections()
        {
            Seed.DropListingCollections(categoriesRepo, eventRepo, businessRepo);
            return Content("Listing collections dropped");
        }

        public IActionResult DropFormCollections()
        {
            Seed.DropFormCollections(eventForms, businessForms);
            return Content("Form collections dropped");
        }
    }
}