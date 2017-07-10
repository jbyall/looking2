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
            //var titlesToAdd = Enumerable.Repeat("", 5-this.Titles.Count)).ToList();
            //var descriptionsToAdd = Enumerable.Repeat("", 10).ToList();
            //var contactToAdd = Enumerable.Repeat("", 5).ToList();
            //var locationsToAdd = Enumerable.Repeat("", 5).ToList();
            while (this.Titles.Count < 5)
            {
                this.Titles.Add("");
            }

            while (this.Descriptions.Count<10)
            {
                this.Descriptions.Add("");
            }

            while (this.Contact.Count < 5)
            {
                this.Contact.Add("");
            }

            while (this.Location.Count<10)
            {
                this.Location.Add("");
            }
        }
    }
}
