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
        private ICategoriesRepository categoryRepo;

        public BusinessesController(IBusinessRepository _businessRepo, ICategoriesRepository _categoryRepo)
        {
            this.businessRepo = _businessRepo;
            this.categoryRepo = _categoryRepo;
        }

        [HttpGet]
        public IActionResult CategoryIndex()
        {
            var eventCategories = categoryRepo.GetByType(CategoryType.Business);
            return View(eventCategories);
        }

        public IActionResult Create()
        {
            var model = new BusinessListing();
            return View(model);
        }
    }
}