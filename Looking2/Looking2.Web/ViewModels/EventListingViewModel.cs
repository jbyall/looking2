using Looking2.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.ViewModels
{
    public class EventListingViewModel : FieldSet
    {
        public EventListingViewModel()
        {
            this.Listing = new EventListing();
        }
        public EventListing Listing { get; set; }
        public EventType Type { get; set; }
        public EventCategory Category { get; set; }
        // Unique labels
        public string PriceLabel { get; set; }
        public string AdmissionInfoLabel { get; set; }
        public string DateLabel { get; set; }

        // Other
        public string PageTitle { get; set; }
        public string HeadingPartial { get; set; }

    }
}
