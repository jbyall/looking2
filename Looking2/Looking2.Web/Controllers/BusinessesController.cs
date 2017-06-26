using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Looking2.Web.DataAccess;
using Looking2.Web.Domain;

namespace Looking2.Web.Controllers
{
    public class BusinessesController : Controller
    {
        private IBusinessRepository businessRepo;

        public BusinessesController(IBusinessRepository repo)
        {
            this.businessRepo = repo;
        }

        public IActionResult Create()
        {
            var model = new BusinessListing();
            return View(model);
        }
    }
}