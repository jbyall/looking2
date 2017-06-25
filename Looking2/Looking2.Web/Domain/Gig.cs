using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    public class Gig
    {
        public ObjectId Id { get; set; }
        public string BandName { get; set; }
        public string Genre { get; set; }
        public string Venue { get; set; }
        public string ArtistUrl { get; set; }
        public string VenueUrl { get; set; }
        public string Price { get; set; }
        public string TicketInfo { get; set; }
        public DateTime? Date { get; set; }
        public string Duration { get; set; }
        public string Time { get; set; }
        public string Brag { get; set; }
        public string LongDescription { get; set; }
    }
}
