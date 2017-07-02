using Looking2.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{

    public enum EventCategory
    {
        LiveMusic,
        Other
    }

    public enum EventType
    {
        Fundraiser,
        Gig,
        ArtistIndividual,
        ArtistMultiple,
        Series,
        Exhibit,
        Concert,
        Orchestra,
        Other
    }
    public class EventListing : Entity
    {
        public string AdmissionInfo { get; set; }
        public string Price { get; set; }
        public DateTime? Date { get; set; }
        public string Brag { get; set; }
        public string LongDescription { get; set; }
        public EventCategory EventCategory { get; set; }
        public EventType EventType { get; set; }

    }
}
