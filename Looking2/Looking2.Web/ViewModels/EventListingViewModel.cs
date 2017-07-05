using Looking2.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.ViewModels
{
    public class EventListingViewModel
    {
        public EventListingViewModel()
        {
            this.Listing = new EventListing();
        }
        public EventListing Listing { get; set; }
        public EventFieldSet FieldSet { get; set; }
    }
}
