using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Looking2.Web.Controllers
{
    public class SeedController : Controller
    {
        public IActionResult Index()
        {
            Seed.SeedDb();
            return Content("Done");
        }
    }
}