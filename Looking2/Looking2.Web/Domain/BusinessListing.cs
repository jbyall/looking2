using Looking2.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Description("Health Care")]
        HealthCare,
        [Description("Alternative Health Care")]
        AltHealthCare,
        Information,
        Instruction,
        Lawyers,
        Restaurant,
        [Description("Service Provider")]
        ServiceProviders,
        Shopkeepers,
        Support
    }
    public class BusinessListing : Listing
    {
        public string Brag { get; set; }
        public string LongDescription { get; set; }
        public BusinessCategory BusinessCategory { get; set; }
        public BusinessType BusinessType { get; set; }
    }
}
