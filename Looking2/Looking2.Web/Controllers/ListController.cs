using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Looking2.Web.DataAccess;

namespace Looking2.Web.Controllers
{
    public class ListController : Controller
    {
        private ICategoriesRepository categoriesRepo;
        public ListController(ICategoriesRepository repo)
        {
            this.categoriesRepo = repo;
        }

        [HttpGet]
        public IActionResult EventIndex()
        {
            var eventCategories = categoriesRepo.GetByType(Domain.CategoryType.Event);
            return View(eventCategories);
        }

        public IActionResult BusinessIndex()
        {
            var businessCategories = categoriesRepo.GetByType(Domain.CategoryType.Business);
            return View(businessCategories);
        }
    }
}