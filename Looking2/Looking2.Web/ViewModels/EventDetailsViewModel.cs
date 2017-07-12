using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Looking2.Web.Domain;
using Looking2.Web.Services;
using System.Globalization;

namespace Looking2.Web.ViewModels
{
    public class EventViewModel
    {
        public EventViewModel()
        {
            this.Listing = new EventListing();
        }
        public EventViewModel(EventListing listing)
        {
            this.Listing = listing;
            this.Id = listing.Id.ToString();
            this.Date = listing.Date.HasValue ? listing.Date.Value.ToString("d") : null;
            this.Type = listing.EventType.ToString();
            this.TitleFormatted = DisplayHelper.ParseEventTitle(listing);
            this.ContactFormatted = DisplayHelper.ParseListingContact(listing.Contact);
            this.Brag = listing.Brag;
            this.AdmissionInfo = listing.AdmissionInfo;
            this.Price = listing.Price;
            this.LongDescription = listing.LongDescription;
            this.DescriptionFormatted = DisplayHelper.ParseListingDescription(listing.Descriptions);
            this.LocationFormatted = DisplayHelper.ParseListingLocation(listing.Location);
        }
        public EventViewModel(EventListing listing, EventFormData formData) : this(listing)
        {
            this.FormData = formData;
        }
        public string Id { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string TitleFormatted { get; set; }
        public string DescriptionFormatted { get; set; }
        public string LocationFormatted { get; set; }
        public string ContactFormatted { get; set; }
        public string Brag { get; set; }
        public string AdmissionInfo { get; set; }
        public string Price { get; set; }
        public string LongDescription { get; set; }
        public int DescriptionCount { get; set; }
        public int TitleCount { get; set; }
        public int LocationCount { get; set; }
        public int ContactCount { get; set; }
        public EventListing Listing { get; set; }
        public EventFormData FormData { get; set; }
    }
}
