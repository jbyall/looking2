using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Looking2.Web.DataAccess;
using Looking2.Web.Domain;
using Looking2.Web.ViewModels;
using Looking2.Web.Services;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;

namespace Looking2.Web.Controllers
{
    public class EventsController : Controller
    {
        private IEventsRepository eventsRepo;
        private ICategoriesRepository categoryRepo;
        private IEventFormsRepo formsRepo;

        public EventsController(IEventsRepository _eventRepo, ICategoriesRepository _categoryRepo, IEventFormsRepo _formsRepo)
        {
            this.eventsRepo = _eventRepo;
            this.categoryRepo = _categoryRepo;
            this.formsRepo = _formsRepo;
        }

        public IActionResult Search(string textQuery, string locationQuery)
        {
            var searchResults = new List<EventListing>();
            if (string.IsNullOrWhiteSpace(textQuery) && string.IsNullOrWhiteSpace(locationQuery))
            {
                searchResults = eventsRepo.GetAll().ToList();
            }
            else
            {
                SearchCriteria criteria = new SearchCriteria(textQuery, locationQuery, textQuery, textQuery);
                searchResults = eventsRepo.SearchListings(criteria);
            }

            var viewListings = new List<EventViewModel>();
            foreach (var item in searchResults)
            {
                viewListings.Add(new EventViewModel(item));
            }
            return PartialView("EventListings", viewListings);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listings = eventsRepo.GetAll();
            var viewListings = new List<EventViewModel>();
            foreach (var item in listings)
            {
                viewListings.Add(new EventViewModel(item));
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
        public IActionResult Create(EventViewModel model)
        {
            eventsRepo.Add(model.Listing);
            return RedirectToAction("CreateLocation", new { id = model.Listing.Id.ToString() });
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var listing = eventsRepo.GetById(id);
            var vm = new EventViewModel(listing);
            return View(vm);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Delete(string id)
        {
            eventsRepo.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var listing = eventsRepo.GetById(id);
            EventViewModel vm = populateModel(listing);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(EventViewModel model)
        {
            model.Listing.Id = new ObjectId(model.Id);
            eventsRepo.Update(model.Listing);
            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpGet]
        public IActionResult CreateLocation(string id)
        {
            var listing = eventsRepo.GetById(id);
            var vm = new EventViewModel(listing);
            return View(vm);
        }

        public IActionResult RenderLocationPartial(string viewName, string listingId)
        {
            var listing = eventsRepo.GetById(listingId);
            var vm = new EventViewModel(listing);
            vm.Listing.Location = Enumerable.Repeat("", 10).ToList();
            if (viewName == "NYC")
            {
                listing.Location[0] = "NY";
                listing.Location[2] = "NYC";
            }
            string viewPath = string.Format("~/Views/Events/LocationPartials/_{0}.cshtml", viewName);
            return PartialView(viewPath, vm);
        }

        [HttpPost]
        public IActionResult CreateLocation(EventViewModel model)
        {
            var listing = eventsRepo.GetById(model.Id);
            foreach (var item in model.Listing.Location)
            {
                listing.Location.Add(item);
            }
            listing = eventsRepo.Update(listing);
            return RedirectToAction("Review", new { id = listing.Id.ToString() });
        }

        public IActionResult Review(string id)
        {
            var listing = eventsRepo.GetById(id);
            var vm = new EventViewModel(listing);
            return View(vm);
        }

        public IActionResult SubmitListing(string id)
        {
            var listing = eventsRepo.GetById(id);
            listing.Status = ListingStatus.Active;
            eventsRepo.Update(listing);
            return RedirectToAction("Index");
        }
        #region helpers
        private EventViewModel getModelByEventType(string eventType)
        {
            var model = new EventViewModel();
            EventType type;
            if (Enum.TryParse(eventType, out type))
            {
                switch (type)
                {
                    case EventType.Gig:
                        model.FormData = formsRepo.GetByName("GigCreate");
                        break;
                    case EventType.ArtistIndividual:
                        model.FormData = formsRepo.GetByName("ArtistIndividualCreate");
                        break;
                    case EventType.ArtistMultiple:
                        model.FormData = formsRepo.GetByName("ArtistMultipleCreate");
                        break;
                    case EventType.Concert:
                        model.FormData = formsRepo.GetByName("ConcertCreate");
                        break;
                    case EventType.Orchestra:
                        model.FormData = formsRepo.GetByName("OrchestraCreate");
                        break;
                    case EventType.Benefit:
                        model.FormData = formsRepo.GetByName("BenefitCreate");
                        break;
                    case EventType.Series:
                        model.FormData = formsRepo.GetByName("SeriesCreate");
                        break;
                    case EventType.Exhibit:
                        model.FormData = formsRepo.GetByName("ExhibitCreate");
                        break;
                    default:
                        model.FormData = formsRepo.GetByName("OtherCreate");
                        break;
                }

                // Set event type
                model.Listing.EventType = type;
            }
            else
            {
                model.FormData = formsRepo.GetByName("OtherCreate");
                model.Listing.EventType = EventType.Other;
            }

            //create empty fields for view
            model.Listing.Initialize();

            return model;
        }

        private EventViewModel populateModel(EventListing listingModel)
        {
            var model = new EventViewModel(listingModel);
            //model.Listing = listingModel;
            switch (listingModel.EventType)
            {
                case EventType.Gig:
                    model.FormData = formsRepo.GetByName("GigCreate");
                    break;
                case EventType.ArtistIndividual:
                    model.FormData = formsRepo.GetByName("ArtistIndividualCreate");
                    break;
                case EventType.ArtistMultiple:
                    model.FormData = formsRepo.GetByName("ArtistMultipleCreate");
                    break;
                case EventType.Concert:
                    model.FormData = formsRepo.GetByName("ConcertCreate");
                    break;
                case EventType.Orchestra:
                    model.FormData = formsRepo.GetByName("OrchestraCreate");
                    break;
                case EventType.Benefit:
                    model.FormData = formsRepo.GetByName("BenefitCreate");
                    break;
                case EventType.Series:
                    model.FormData = formsRepo.GetByName("SeriesCreate");
                    break;
                case EventType.Exhibit:
                    model.FormData = formsRepo.GetByName("ExhibitCreate");
                    break;
                default:
                    model.FormData = formsRepo.GetByName("OtherCreate");
                    break;
            }
            model.Listing.Initialize();
            return model;
        }
        #endregion
    }
}