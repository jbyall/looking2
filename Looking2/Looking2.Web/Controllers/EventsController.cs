using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Looking2.Web.DataAccess;
using Looking2.Web.Domain;
using Looking2.Web.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Looking2.Web.Controllers
{
    public class EventsController : Controller
    {
        private IEventsRepository eventsRepo;
        private ICategoriesRepository categoryRepo;
        private IConfiguration configuration;
        //private EventListingViewModel vmFields;
        public EventsController(IEventsRepository _eventRepo, ICategoriesRepository _categoryRepo, IConfiguration _configuration)
        {
            this.eventsRepo = _eventRepo;
            this.categoryRepo = _categoryRepo;
            this.configuration = _configuration;
        }

        public IActionResult Index()
        {
            var listings = eventsRepo.GetAll();
            var viewListings = new List<EventDetailsViewModel>();
            foreach (var item in listings)
            {
                viewListings.Add(new EventDetailsViewModel(item));
            }
            return View(viewListings);
        }

        [HttpGet]
        public IActionResult CategoryIndex()
        {
            var eventCategories = categoryRepo.GetByType(CategoryType.Event);
            return View(eventCategories);
        }

        public IActionResult Create(string eventType)
        {
            var model = new EventListingViewModel();
            
            EventType type;
            if(Enum.TryParse(eventType, out type))
            {
                switch (type)
                {
                    
                    case EventType.Gig:
                        // Populate field values from app settings
                        configuration.Bind("GigFieldLabels", model);
                        model.Category = EventCategory.LiveMusic;
                        break;
                    case EventType.ArtistIndividual:
                    case EventType.ArtistMultiple:
                    case EventType.Concert:
                    case EventType.Orchestra:
                        model.Category = EventCategory.LiveMusic;
                        break;
                    default:
                        model.Category = EventCategory.Other;
                        break;
                }

                // Set event type
                model.Type = type;
            }
            else
            {
                model.Category = EventCategory.Other;
            }

            //create empty fields for view
            model.Listing.Initialize();

            return View(eventType+"Create", model);
        }

        [HttpPost]
        public IActionResult Create(EventListingViewModel model)
        {
            model.Listing.Clean();
            eventsRepo.Add(model.Listing);
            return RedirectToAction("Details", new { id = model.Listing.Id.ToString()});
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var listing = eventsRepo.GetById(id);
            var vm = new EventDetailsViewModel(listing);
            return View(vm);
            //return View(model.EventType.ToString() + "Details", model);
        }

        #region helpers

        #endregion
    }
}