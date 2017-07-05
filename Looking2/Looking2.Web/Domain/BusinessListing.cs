using Looking2.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    
    public class BusinessListing : Listing
    {
        public string Brag { get; set; }
        public string LongDescription { get; set; }
        public string BusinessDescription { get; set; }
        public BusinessType BusinessType { get; set; }
    }
}
