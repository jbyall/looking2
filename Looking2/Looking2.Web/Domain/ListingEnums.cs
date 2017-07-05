using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    public enum EventDescription
    {
        LiveMusic,
        Fundraiser,
        Benefit,

        Other
    }

    public enum BusinessDescription
    {
        Restaurant,
        Bar,
        [Description("Restaurant & bar")]
        RestaurantBar,
        [Description("Attorney")]
        Lawyer,
        Other
    }

    public enum EventType
    {
        Benefit,
        Gig,
        [Description("Artist")]
        ArtistIndividual,
        [Description("Multiple Artists")]
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
