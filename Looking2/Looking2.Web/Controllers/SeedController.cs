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
            ViewData["MigrationSummary"] = "Change display text of individual artist category";
            return View();
        }

        public IActionResult RunMigration()
        {
            try
            {
                var indArtistCat = categoriesRepo.GetByName("ArtistIndividual").SingleOrDefault();
                indArtistCat.DisplayName = "Solo Acts, Duos & Trios";
                indArtistCat.Description = "for performers taking the stage alone or joining with others for the event being listed";
                categoriesRepo.Update(indArtistCat);

                var multipleArtist = categoriesRepo.GetByName("ArtistMultiple").SingleOrDefault();
                multipleArtist.Description = "covers events with multiple performers";
                categoriesRepo.Update(multipleArtist);

                var tours = categoriesRepo.GetByName("Concert").SingleOrDefault();
                tours.DisplayName = "Tours";
                categoriesRepo.Update(tours);

                var orchestras = categoriesRepo.GetByName("Orchestra").SingleOrDefault();
                orchestras.DisplayName = "Ensembles";
                categoriesRepo.Update(orchestras);

                var otherCategories = categoriesRepo.GetByName("Other").ToList();
                foreach (var item in otherCategories)
                {
                    item.DisplayName = "OTHER";
                    categoriesRepo.Update(item);
                }

                ViewData["Result"] = "Success";
                return View();
            }
            catch (Exception)
            {
                ViewData["Result"] = "Exception";
                return View();
            }
            

        }

        
    }
}