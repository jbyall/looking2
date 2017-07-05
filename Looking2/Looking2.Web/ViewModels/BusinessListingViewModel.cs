using Looking2.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.ViewModels
{
    public class BusinessListingViewModel
    {
        public BusinessListingViewModel()
        {

        }
        // Represents the listing object that is saved to the listings collection
        public BusinessListing Listing { get; set; }

        // Represents the labels for the form inputs (these differ by business)
        public BusinessFormData FormData { get; set; }
    }
}
