using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.DataAccess
{
    public class TextFieldSearch
    {
        public string FieldName { get; set; }
        public string Value { get; set; }
    }
    public class SearchCriteria
    {
        public SearchCriteria(string titleFilter, string locationFilter, string descriptionFilter, string venueFilter = null, int maxResults = 100)
        {
            this.DetailFilters = new List<TextFieldSearch>();
            if (!string.IsNullOrWhiteSpace(titleFilter))
            {
                DetailFilters.Add(new TextFieldSearch { FieldName="Titles", Value=titleFilter });
            }

            if (!string.IsNullOrWhiteSpace(descriptionFilter))
            {
                DetailFilters.Add(new TextFieldSearch { FieldName = "Descriptions", Value = descriptionFilter });
            }

            if (!string.IsNullOrWhiteSpace(venueFilter))
            {
                DetailFilters.Add(new TextFieldSearch { FieldName = "Venue", Value = venueFilter });
            }

            this.LocationFilter = locationFilter;
            this.MaxResults = maxResults;
        }

        public List<TextFieldSearch> DetailFilters { get; set; }
        public string LocationFilter { get; set; }
        public int MaxResults { get; set; }
        public int Skip { get; set; }

    }
}
