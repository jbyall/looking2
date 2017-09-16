using System;
using System.Collections.Generic;
using System.Text;
using Looking2.Web.Domain;

namespace Looking2Console
{
    public class BusinessCategoryResult
    {
        public BusinessCategoryResult()
        {
            this.TypeCount = new Dictionary<BusinessSearchCategory, int>();
        }
        public string ListingType { get; set; }
        public Dictionary<BusinessSearchCategory, int> TypeCount { get; set; }
    }

    
}
