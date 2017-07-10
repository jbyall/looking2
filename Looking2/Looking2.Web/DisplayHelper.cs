using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Looking2.Web.Domain;

namespace Looking2.Web
{
    public static class DisplayHelper
    {
        public static string ParseListingDescription(List<string> descriptions)
        {
            return parseDescription(descriptions);
        }

        public static string ParseListingContact(List<string> contacts)
        {
            return parseContact(contacts);
        }

        public static string ParseListingLocation(List<string> locations)
        {
            return parseLocation(locations);
        }

        public static string ParseBusinessTitle(List<string> titles)
        {
            return parseBusinessTitle(titles);
        }

        public static string ParseEventTitle(EventListing listing)
        {
            return parseEventTitles(listing);

        }

        private static string parseDescription(List<string> descriptions)
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

        private static string parseContact(List<string> contacts)
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

        private static string parseLocation(List<string> locations)
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

        private static string parseBusinessTitle(List<string> titles)
        {
            var result = "";
            foreach (var item in titles)
            {
                result += item;
            }
            return result;
        }

        public static string parseEventTitles(EventListing listing)
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
