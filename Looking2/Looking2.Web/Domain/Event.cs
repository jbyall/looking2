using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    public enum EventType
    {
        Fundraiser,
        Gig,
        SoloArtist,
        MultipleArtist,
        Series,
        Exhibit,
        Concert,
        Orchestra,
        Other
    }

    public class Event
    {
        public ObjectId Id{ get; set; }
        public EventType Type { get; set; }
        public int MyProperty { get; set; }
    }
}
