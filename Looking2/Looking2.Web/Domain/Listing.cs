using Looking2.Web.DataAccess;
using Looking2.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    public abstract class Listing : Entity
    {
        public Listing()
        {
            this.Titles = new List<string>();
            this.Descriptions = new List<string>();
            this.Contact = new List<string>();
            this.Status = ListingStatus.InProgress;
        }
        public ListingStatus Status { get; set; }
        public List<string> Titles { get; set; }
        public List<string> Descriptions { get; set; }
        public List<string> Contact { get; set; }
        public List<string> Location { get; set; }

        /// <summary>
        /// Creates empty fields for lists to use in view
        /// </summary>
        public void Initialize()
        {
            this.Titles = Enumerable.Repeat("", 5).ToList();
            this.Descriptions = Enumerable.Repeat("", 10).ToList();
            this.Contact = Enumerable.Repeat("", 5).ToList();
            this.Location = Enumerable.Repeat("", 5).ToList();
        }
    }
}
