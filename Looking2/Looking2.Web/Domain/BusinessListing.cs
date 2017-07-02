using Looking2.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    public class BusinessListing : Entity
    {
        public string Brag { get; set; }
        public string LongDescription { get; set; }
        public EventCategory EventCategory { get; set; }
        public EventType EventType { get; set; }
    }
}
