using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Looking2.Web.DataAccess;
using Looking2.Web.Domain;
using Looking2.Web.ViewModels;
using Looking2.Web.Services;

namespace Looking2.Web.Controllers
{
    public class EventsController : Controller
    {
        private IEventsRepository eventsRepo;
        private ICategoriesRepository categoryRepo;
        private IEventFormsRepo formsRepo;
        private ISearchOverride overrides;

        public EventsController(IEventsRepository _eventRepo, ICategoriesRepository _categoryRepo, IEventFormsRepo _formsRepo, ISearchOverride _overrides)
        {
            this.eventsRepo = _eventRepo;
            this.categoryRepo = _categoryRepo;
            this.formsRepo = _formsRepo;
            this.overrides = _overrides;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listings = eventsRepo.GetAll();
            var viewListings = new List<EventDetailsViewModel>();
            foreach (var item in listings)
            {
                viewListings.Add(new EventDetailsViewModel(item, overrides));
            }
            return View(viewListings);
        }

        [HttpGet]
        public IActionResult CategoryIndex()
        {
            var eventCategories = categoryRepo.GetByType(ListingCategory.Event);
            return View(eventCategories);
        }

        [HttpGet]
        public IActionResult Create(string eventType)
        {
            var model = getModelByEventType(eventType);
            return View(model);
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
            var vm = new EventDetailsViewModel(listing, overrides);
            return View(vm);
        }

        #region helpers
        private EventListingViewModel getModelByEventType(string eventType)
        {
            var model = new EventListingViewModel();
            EventType type;
            if (Enum.TryParse(eventType, out type))
            {
                switch (type)
                {
                    case EventType.Gig:
                        model.FormData = formsRepo.GetByName("GigCreate");
                        //model.Listing.SearchDescription = EventDescription.LiveMusic.ToString();
                        break;
                    case EventType.ArtistIndividual:
                        model.FormData = formsRepo.GetByName("ArtistIndividualCreate");
                        //model.Listing.SearchDescription = EventDescription.LiveMusic.ToString();
                        break;
                    case EventType.ArtistMultiple:
                        model.FormData = formsRepo.GetByName("ArtistMultipleCreate");
                        //model.Listing.SearchDescription = EventDescription.LiveMusic.ToString();
                        break;
                    case EventType.Concert:
                        model.FormData = formsRepo.GetByName("ConcertCreate");
                        //model.Listing.SearchDescription = EventDescription.LiveMusic.ToString();
                        break;
                    case EventType.Orchestra:
                        model.FormData = formsRepo.GetByName("OrchestraCreate");
                        //model.Listing.SearchDescription = EventDescription.LiveMusic.ToString();
                        break;
                    case EventType.Benefit:
                        model.FormData = formsRepo.GetByName("BenefitCreate");
                        //model.Listing.SearchDescription = EventDescription.Other.ToString();
                        break;
                    case EventType.Series:
                        model.FormData = formsRepo.GetByName("SeriesCreate");
                        //model.Listing.SearchDescription = EventDescription.Other.ToString();
                        break;
                    case EventType.Exhibit:
                        model.FormData = formsRepo.GetByName("ExhibitCreate");
                        //model.Listing.SearchDescription = EventDescription.Other.ToString();
                        break;
                    default:
                        model.FormData = formsRepo.GetByName("OtherCreate");
                        //model.Listing.SearchDescription = EventDescription.Other.ToString();
                        break;
                }

                // Set event type
                model.Listing.EventType = type;
            }
            else
            {
                model.FormData = formsRepo.GetByName("OtherCreate");
                //model.Listing.SearchDescription = EventDescription.Other.ToString();
                model.Listing.EventType = EventType.Other;
            }

            //create empty fields for view
            model.Listing.Initialize();

            return model;
        }
        #endregion
    }
}