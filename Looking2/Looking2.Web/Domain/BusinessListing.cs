using Looking2.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    public enum BusinessCategory
    {
        Other
    }

    public enum BusinessType
    {
        Artists,
        HealthCare,
        AltHealthCare,
        Information,
        Instruction,
        Lawyers,
        Restaurant,
        ServiceProviders,
        Shopkeepers,
        Support
    }
    public class BusinessListing : Entity
    {
        public string Brag { get; set; }
        public string LongDescription { get; set; }
        public BusinessCategory BusinessCategory { get; set; }
        public BusinessType BusinessType { get; set; }
    }
}
