using Looking2.Web.DataAccess;
using Looking2.Web.Services;
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
        public string Venue { get; set; }
        public EventType EventType { get; set; }

        /// <summary>
        /// Manipulate properties for data entry.
        /// </summary>
        /// <param name="dataCleaner"></param>
        public void Listify(IListingCleaner descriptionOverrides)
        {
            this.clean();
            this.setDescriptions(descriptionOverrides);
        }

        // Removes empty list items
        private void clean()
        {
            try
            {
                this.Titles.RemoveAll(t => string.IsNullOrWhiteSpace(t));
            }
            catch { }

            try
            {
                this.Descriptions.RemoveAll(d => string.IsNullOrWhiteSpace(d));
            }
            catch { }
            try
            {
                this.Contact.RemoveAll(c => string.IsNullOrWhiteSpace(c));
            }
            catch { }
            try
            {
                this.Location.RemoveAll(l => string.IsNullOrWhiteSpace(l));
            }
            catch { }
        }

        // Sets a default description if necessary
        private void setDescriptions(IListingCleaner overrides)
        {
            string descriptionToAdd;
            if (overrides.EventDescriptionOverrides.TryGetValue(this.EventType, out descriptionToAdd))
            {
                if (this.Descriptions.First() != descriptionToAdd)
                {
                    this.Descriptions.Insert(0, descriptionToAdd);
                }
            }
        }

    }
}
