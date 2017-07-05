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

        public BusinessListing Listing { get; set; }
        public BusinessFieldSet FieldSet { get; set; }
    }
}
