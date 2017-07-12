using Looking2.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Looking2.Web.Services;

namespace Looking2.Web.ViewModels
{
    public class BusinessViewModel
    {
        public BusinessViewModel()
        {
            this.Listing = new BusinessListing();
        }
        public BusinessViewModel(BusinessListing listing)
        {
            this.Listing = listing;
            this.Id = listing.Id.ToString();
            this.Type = listing.BusinessType.ToString();
            this.TitleFormatted = DisplayHelper.ParseBusinessTitle(listing.Titles);
            this.ContactFormatted = DisplayHelper.ParseListingContact(listing.Contact);
            this.DescriptionFormatted = DisplayHelper.ParseListingDescription(listing.Descriptions);
            this.LocationFormatted = DisplayHelper.ParseListingLocation(listing.Location);
            this.Brag = listing.Brag;
            this.LongDescription = listing.LongDescription;
            this.BusinessType = listing.BusinessType.ToString();
            
        }
        public BusinessViewModel(BusinessListing listing, BusinessFormData formData) : this(listing)
        {
            this.FormData = FormData;
        }

        public string Id { get; set; }
        public string Type { get; set; }
        public string TitleFormatted { get; set; }
        public string DescriptionFormatted { get; set; }
        public string LocationFormatted { get; set; }
        public string ContactFormatted { get; set; }
        public string Brag { get; set; }
        public string LongDescription { get; set; }
        public string BusinessType { get; set; }
        public BusinessListing Listing { get; set; }
        public BusinessFormData FormData { get; set; }

    }
}
