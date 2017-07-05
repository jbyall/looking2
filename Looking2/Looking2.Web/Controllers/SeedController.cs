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
    [Authorize(Roles = "admin")]
    public class SeedController : Controller
    {
        private IOptions<DbSettings> settings;
        private ICategoriesRepository categoriesRepo;
        private IEventFormsRepo eventForms;
        private IBusinessFormsRepo businessForms;

        public SeedController(IOptions<DbSettings> _settings, 
                                ICategoriesRepository _categoriesRepo,
                                IBusinessFormsRepo _businessForms,
                                IEventFormsRepo _eventForms)
        {
            this.settings = _settings;
            this.categoriesRepo = _categoriesRepo;
            this.businessForms = _businessForms;
            this.eventForms = _eventForms;
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

        public IActionResult SeedAll()
        {
            Seed.SeedAll(categoriesRepo, businessForms, eventForms);
            return Content("All done.");
        }
    }
}