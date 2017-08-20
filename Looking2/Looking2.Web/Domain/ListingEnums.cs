using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    public enum ListingStatus
    {
        Expired,
        Active,
        InProgress,
        UnderReview,
        Rejected
    }
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
        Support,
        Other
    }

    public enum BusinessSearchCategory
    {
        All = 0,
        [Display(Name = "Auto/Vehicle")]
        AutoVehicle = 1,

        [Display(Name = "Entertainment & Attractions")]
        EntertainmentAttractions = 2,

        [Display(Name = "Food & Beverage")]
        FoodAndBeverage = 3,

        [Display(Name = "General Services")]
        Services = 4,

        [Display(Name = "Healthcare")]
        Healthcare = 5,

        [Display(Name = "Home Goods & Services")]
        HomeGoodsAndServices = 6,

        [Display(Name = "Human Services")]
        HumanServices = 7,

        [Display(Name = "Lawyers")]
        Lawyers = 8,

        [Display(Name = "Pet & Animal")]
        PetAnimal = 9,

        [Display(Name = "Public Services")]
        PublicServices = 10,

        [Display(Name = "Retail & Shops")]
        Retail = 11,

        [Display(Name = "Salons, Parlors, Beauty")]
        Salons = 12,

        [Display(Name = "Sports & Fitness")]
        SportFitness = 13,

        [Display(Name = "Supplies")]
        Supplies = 14,

        [Display(Name = "Tutoring & Education")]
        Lessons = 15

    }
}
