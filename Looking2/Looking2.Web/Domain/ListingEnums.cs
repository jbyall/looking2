using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    //public enum EventDescription
    //{
    //    [Description("Live Music")]
    //    LiveMusic,
    //    [Description("Benefit/Fundraiser")]
    //    BenefitFundraiser,
    //    Other
    //}

    //public enum BusinessDescription
    //{
    //    Restaurant,
    //    Bar,
    //    [Description("Restaurant & bar")]
    //    RestaurantBar,
    //    [Description("Attorney")]
    //    Lawyer,
    //    Other
    //}

    public enum EventType
    {
        Benefit,
        [Description("Live Music")]
        Gig,
        [Description("Live Music")]
        ArtistIndividual,
        [Description("Live Music")]
        ArtistMultiple,
        Series,
        Exhibit,
        Concert,
        Orchestra,
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
}
