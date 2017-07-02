using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Looking2.Web.DataAccess;
using Looking2.Web.Domain;
using Looking2.Web.ViewModels;

namespace Looking2.Web.Controllers
{
    public class EventsController : Controller
    {
        private IEventsRepository eventsRepo;
        private ICategoriesRepository categoryRepo;
        public EventsController(IEventsRepository _eventRepo, ICategoriesRepository _categoryRepo)
        {
            this.eventsRepo = _eventRepo;
            this.categoryRepo = _categoryRepo;
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
            var model = new EventListing();
            EventType type;
            if(Enum.TryParse(eventType, out type))
            {
                switch (type)
                {
                    
                    case EventType.Gig:
                        model.EventCategory = EventCategory.LiveMusic;
                        break;
                    case EventType.ArtistIndividual:
                    case EventType.ArtistMultiple:
                    case EventType.Concert:
                    case EventType.Orchestra:
                        model.EventCategory = EventCategory.LiveMusic;
                        break;
                    default:
                        model.EventCategory = EventCategory.Other;
                        break;
                }

                // Set event type
                model.EventType = type;
            }
            else
            {
                model.EventCategory = EventCategory.Other;
            }

            //create empty fields for view
            model.Initialize();

            return View(eventType+"Create", model);
        }

        [HttpPost]
        public IActionResult Create(EventListing model)
        {
            model.Clean();
            eventsRepo.Add(model);
            return RedirectToAction("Details", new { id = model.Id.ToString()});
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