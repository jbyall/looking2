using Looking2.Web.DataAccess;
using Looking2.Web.Services;
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
        public BusinessType BusinessType { get; set; }

        /// <summary>
        /// Manipulate properties for data entry.
        /// </summary>
        /// <param name="dataCleaner"></param>
        public void Listify(IListingCleaner dataCleaner)
        {
            this.clean();
            this.setDescriptions(dataCleaner);
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
            if (overrides.BusinessDescriptionOverrides.ContainsKey(this.BusinessType))
            {
                this.Descriptions.Insert(0, overrides.BusinessDescriptionOverrides[this.BusinessType]);
            }
        }
    }
}
