using Looking2.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Looking2.Web.Services;

namespace Looking2.Web.ViewModels
{
    public class BusinessDetailsViewModel
    {
        public BusinessDetailsViewModel()
        {
            this.Listing = new BusinessListing();
        }
        public BusinessDetailsViewModel(BusinessListing listing)
        {
            this.Id = listing.Id.ToString();
            this.Type = listing.BusinessType.ToString();
            this.Title = DisplayHelper.ParseBusinessTitle(listing.Titles);
            
            this.Contact = DisplayHelper.ParseListingContact(listing.Contact);
            this.Brag = listing.Brag;
            this.LongDescription = listing.LongDescription;
            this.BusinessType = listing.BusinessType.ToString();
            this.Description = DisplayHelper.ParseListingDescription(listing.Descriptions);
        }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Brag { get; set; }
        public string LongDescription { get; set; }
        public string BusinessType { get; set; }
        public BusinessListing Listing { get; set; }

    }
}
