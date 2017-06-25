using Looking2.Web.DataAccess;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    public enum CategoryType
    {
        Business,
        Event
    };

    public class Category : Entity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public CategoryType Type { get; set; }
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }
    }

    
}
