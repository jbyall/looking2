using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Looking2.Web.Domain;
using Looking2.Web.Services;
using Looking2.Web.DataAccess;
using Microsoft.Extensions.Options;

namespace Looking2.Web
{
    public interface IDisplayHelper
    {
        IListingCleaner Cleaner { get; }
        string ParseListingDescription(List<string> descriptions);
        string ParseListingContact(List<string> contacts);
        string ParseListingLocation(List<string> locations);
        string ParseBusinessTitle(List<string> titles);
        string ParseEventTitle(EventListing listing);
    }
    public class DisplayHelper
    {
        public DisplayHelper(IListingCleaner _cleaner)
        {
            this.Cleaner = _cleaner;
        }

        public IListingCleaner Cleaner { get; set; }

        public static string ParseListingDescription(List<string> descriptions)
        {
            var result = "";
            if (descriptions.Count > 0)
            {
                result = descriptions[0];
            }
            var count = descriptions.Count < 5 ? descriptions.Count : 5;
            for (int i = 1; i < count; i++)
            {
                result += " | " + descriptions[i];
            }
            return result;
        }

        public static string ParseListingContact(List<string> contacts)
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

        public static string ParseListingLocation(List<string> locations)
        {
            if (locations == null)
            {
                return "";
            }
            string result = "";
            var count = locations.Count;
            switch (count)
            {
                case 0:
                case 1:
                    break;
                default:
                    result += string.Format("{0} ({1})", locations[0], locations[1]);
                    break;

            }

            for (int i = 2; i < count; i++)
            {
                result += " | " + locations[i];
            }

            return result;
        }

        public static string ParseBusinessTitle(List<string> titles)
        {
            var result = "";
            foreach (var item in titles)
            {
                result += item;
            }
            return result;
        }

        public static string ParseEventTitle(EventListing listing)
        {
            var result = string.Join(" ", listing.Titles);
            if (!string.IsNullOrWhiteSpace(listing.Venue))
            {
                result += " at " + listing.Venue;
            }
            return result;

        }

    }
}
