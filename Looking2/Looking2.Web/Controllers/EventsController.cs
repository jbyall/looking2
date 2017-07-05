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
using Looking2.Web.Settings;
using MongoDB.Driver;

namespace Looking2.Web.Controllers
{
    public class EventsController : Controller
    {
        private IEventsRepository eventsRepo;
        private ICategoriesRepository categoryRepo;
        private IConfiguration configuration;
        private ConnectionStrings connectionStrings;
        //private EventListingViewModel vmFields;
        public EventsController(IEventsRepository _eventRepo, ICategoriesRepository _categoryRepo, IConfiguration _configuration, IOptions<ConnectionStrings> _connectionStrings)
        {
            this.eventsRepo = _eventRepo;
            this.categoryRepo = _categoryRepo;
            this.configuration = _configuration;
            this.connectionStrings = _connectionStrings.Value;
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
            var db = new Looking2DbContext(connectionStrings.Looking2DbConnection);
            var model = new EventListingViewModel();
            
            EventType type;
            if(Enum.TryParse(eventType, out type))
            {
                switch (type)
                {
                    
                    case EventType.Gig:
                        // Populate field values from app settings
                        //configuration.Bind("GigFieldLabels", model);
                        model.FieldSet = db.EventForms.Find(f => f.FormName == "GigCreate").SingleOrDefault();
                        model.Listing.EventCategory = EventCategory.LiveMusic;
                        break;
                    case EventType.ArtistIndividual:
                        model.FieldSet = db.EventForms.Find(f => f.FormName == "ArtistIndividualCreate").SingleOrDefault();
                        model.Listing.EventCategory = EventCategory.LiveMusic;
                        break;
                    case EventType.ArtistMultiple:
                        model.FieldSet = db.EventForms.Find(f => f.FormName == "ArtistMultipleCreate").SingleOrDefault();
                        model.Listing.EventCategory = EventCategory.LiveMusic;
                        break;
                    case EventType.Concert:
                        model.FieldSet = db.EventForms.Find(f => f.FormName == "ConcertCreate").SingleOrDefault();
                        model.Listing.EventCategory = EventCategory.LiveMusic;
                        break;
                    case EventType.Orchestra:
                        model.FieldSet = db.EventForms.Find(f => f.FormName == "OrchestraCreate").SingleOrDefault();
                        model.Listing.EventCategory = EventCategory.LiveMusic;
                        break;
                    case EventType.Benefit:
                        model.FieldSet = db.EventForms.Find(f => f.FormName == "BenefitCreate").SingleOrDefault();
                        model.Listing.EventCategory = EventCategory.Other;
                        break;
                    case EventType.Series:
                        model.FieldSet = db.EventForms.Find(f => f.FormName == "SeriesCreate").SingleOrDefault();
                        model.Listing.EventCategory = EventCategory.Other;
                        break;
                    case EventType.Exhibit:
                        model.FieldSet = db.EventForms.Find(f => f.FormName == "ExhibitCreate").SingleOrDefault();
                        model.Listing.EventCategory = EventCategory.Other;
                        break;
                    default:
                        model.FieldSet = db.EventForms.Find(f => f.FormName == "OtherCreate").SingleOrDefault();
                        model.Listing.EventCategory = EventCategory.Other;
                        break;
                }

                // Set event type
                model.Listing.EventType = type;
            }
            else
            {
                model.FieldSet = db.EventForms.Find(f => f.FormName == "OtherCreate").SingleOrDefault();
                model.Listing.EventCategory = EventCategory.Other;
                model.Listing.EventType = EventType.Other;
            }

            //create empty fields for view
            model.Listing.Initialize();

            return View(model);
            //return View(eventType+"Create", model);
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