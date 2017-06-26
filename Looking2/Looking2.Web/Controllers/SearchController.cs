using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Looking2.Web.DataAccess;

namespace Looking2.Web.Controllers
{
    public class SearchController : Controller
    {
        private IEventsRepository eventsRepo;
        private IBusinessRepository businessRepo;
        private ICategoriesRepository categoriesRepo;

        public SearchController(IEventsRepository _eventsRepo, IBusinessRepository _businessesRepo, ICategoriesRepository _catRepo)
        {
            this.eventsRepo = _eventsRepo;
            this.businessRepo = _businessesRepo;
            this.categoriesRepo = _catRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}