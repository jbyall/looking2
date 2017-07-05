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
        private ConnectionStrings connectionStrings;
        public SeedController(IOptions<ConnectionStrings> _connectionStrings)
        {
            this.connectionStrings = _connectionStrings.Value;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SeedCategories()
        {
            var context = new Looking2DbContext(connectionStrings.Looking2DbConnection);
            Seed.SeedCategories(context);
            return Content("Categories seeded successfully");
        }

        public IActionResult SeedForms()
        {
            var context = new Looking2DbContext(connectionStrings.Looking2DbConnection);
            Seed.SeedForms(context);
            return Content("Forms seeded successfully");
        }

        public IActionResult SeedAll()
        {
            var context = new Looking2DbContext(connectionStrings.Looking2DbConnection);
            Seed.SeedAll(context);
            return Content("All done.");
        }
    }
}