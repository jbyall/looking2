using Looking2.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.ViewModels
{
    public class BusinessDetailsViewModel
    {
        public BusinessDetailsViewModel(BusinessListing listing)
        {
            this.Id = listing.Id.ToString();
            this.Type = listing.BusinessType.ToString();
            this.Title = parseTitle(listing.Titles);
            this.Description = parseDescription(this.Type, listing.Descriptions);
            this.Contact = parseContact(listing.Contact);
            this.Brag = listing.Brag;
            this.LongDescription = listing.LongDescription;
            this.BusinessCategory = listing.BusinessDescription.ToString();
            this.BusinessType = listing.BusinessType.ToString();
        }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Brag { get; set; }
        public string LongDescription { get; set; }
        public string BusinessCategory { get; set; }
        public string BusinessType { get; set; }


        private string parseTitle(List<string> titles)
        {
            var result = "";
            foreach (var item in titles)
            {
                result += item;
            }
            return result;
        }

        private string parseDescription(string type, List<string> descriptions)
        {
            var result = type;
            var count = descriptions.Count < 5 ? descriptions.Count : 5;
            for (int i = 0; i < count; i++)
            {
                result += " | " + descriptions[i];
            }
            return result;
        }

        private string parseContact(List<string> contacts)
        {
            var result = "";
            if (contacts.Count > 0)
            {
                result = contacts[0];
            }
            for (int i = 1; i < contacts.Count; i++)
            {
                result += " | " + contacts[i];
            }
            return result;
        }
    }
}
