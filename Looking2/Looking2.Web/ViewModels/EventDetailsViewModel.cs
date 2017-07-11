using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Looking2.Web.Domain;
using Looking2.Web.Services;

namespace Looking2.Web.ViewModels
{
    public class EventDetailsViewModel
    {
        public EventDetailsViewModel()
        {
            this.Listing = new EventListing();
        }
        public EventDetailsViewModel(EventListing listing)
        {
            this.Listing = listing;
            this.Id = listing.Id.ToString();
            this.Date = listing.Date.HasValue ? listing.Date.Value.ToString("d") : null;
            this.Type = listing.EventType.ToString();
            this.Title = DisplayHelper.ParseEventTitle(listing);
            this.Contact = DisplayHelper.ParseListingContact(listing.Contact);
            this.Brag = listing.Brag;
            this.AdmissionInfo = listing.AdmissionInfo;
            this.Price = listing.Price;
            this.LongDescription = listing.LongDescription;
            this.EventType = listing.EventType.ToString();
            this.Description = DisplayHelper.ParseListingDescription(listing.Descriptions);
            this.Location = DisplayHelper.ParseListingLocation(listing.Location);
        }
        public string Id { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Brag { get; set; }
        public string AdmissionInfo { get; set; }
        public string Price { get; set; }
        public string LongDescription { get; set; }
        public string EventType { get; set; }
        public EventListing Listing { get; set; }
        public bool HasOverride { get; set; }
    }
}
