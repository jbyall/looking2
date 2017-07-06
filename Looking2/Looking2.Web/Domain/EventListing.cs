using Looking2.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{    
    public class EventListing : Listing
    {
        // NOTE - See parent class for generic listing fields

        // Event specific fields
        public string AdmissionInfo { get; set; }
        public string Price { get; set; }
        public DateTime? Date { get; set; }
        public string Brag { get; set; }
        public string LongDescription { get; set; }
        public EventType EventType { get; set; }
    }
}
