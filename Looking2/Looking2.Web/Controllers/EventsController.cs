using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Looking2.Web.ViewModels;
using Looking2.Web.DataAccess;
using Looking2.Web.Domain;

namespace Looking2.Web.Controllers
{
    public class EventsController : Controller
    {
        private IEventsRepository eventsRepo;
        public EventsController(IEventsRepository repo)
        {
            this.eventsRepo = repo;
        }

        public IActionResult Create(string eventType)
        {
            var model = new EventListing();
            EventType type;
            if(Enum.TryParse<EventType>(eventType, out type))
            {
                switch (type)
                {
                    
                    case EventType.Gig:
                        model.EventCategory = EventCategory.LiveMusic;
                        model.Titles = Enumerable.Repeat("", 2).ToList();
                        model.Descriptions = Enumerable.Repeat("", 5).ToList();
                        model.ContactWebsites = Enumerable.Repeat("", 2).ToList();
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
            }
            else
            {
                model.EventCategory = EventCategory.Other;
            }
            
            return View("Create"+eventType, model);
        }

        [HttpPost]
        public IActionResult Create(EventListing model)
        {
            eventsRepo.Add(model);
            return RedirectToAction("Details");
        }

        public IActionResult Details(string id)
        {
            return View();
        }
    }
}