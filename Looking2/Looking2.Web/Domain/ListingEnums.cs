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
        Art = 1,
        [Display(Name = "Auto/Vehicle")]
        AutoVehicle = 2,
        [Display(Name = "Entertainment & Attractions")]
        EntertainmentAttractions = 3,
        [Display(Name = "Healthcare - Traditional")]
        Healthcare = 4,
        [Display(Name = "Healthcare - Alternative")]
        HealthcareAlt = 5,
        [Display(Name = "Instruction & Education")]
        Lessons = 6,
        [Display(Name = "Lawyers")]
        Lawyers = 7,
        Music = 8,
        [Display(Name = "Pet & Animal")]
        PetAnimal = 9,
        [Display(Name = "Restaurants, Food & Drink")]
        FoodAndBeverage = 10,
        [Display(Name = "Retail & Shops")]
        Retail = 11,
        [Display(Name = "Salons, Parlors, Beauty")]
        Salons = 12,
        Services = 13,
        [Display(Name = "Sports & Fitness")]
        SportFitness = 14,
        [Display(Name = "Supplies")]
        Supplies = 15,
        [Display(Name = "Support & Information")]
        SupportInformation = 16
        //[Display(Name = "Services - General")]
        //GeneralServices = 13,
        //[Display(Name = "Services - Home")]
        //HomeServices = 14,
        //[Display(Name = "Services - Human")]
        //HumanServices = 15,
        //[Display(Name = "Services - Public")]
        //PublicServices = 16,
    }

    //public enum ServiceCategory
    //{
    //    All = BusinessSearchCategory.All,
    //    [Display(Name = "Auto/Vehicle")]
    //    AutoVehicle = BusinessSearchCategory.AutoVehicle,

    //    [Display(Name = "Healthcare - Traditional")]
    //    Healthcare = BusinessSearchCategory.Healthcare,

    //    [Display(Name = "Healthcare - Alternative")]
    //    HealthcareAlt = BusinessSearchCategory.HealthcareAlt,

    //    [Display(Name = "Instruction & Education")]
    //    InstructionEducation = BusinessSearchCategory.Lessons,

    //    [Display(Name = "Lawyers")]
    //    Lawyers = BusinessSearchCategory.Lawyers,

    //    [Display(Name = "Pet & Animal")]
    //    PetAnimal = BusinessSearchCategory.PetAnimal,

    //    [Display(Name = "Restaurant, Food & Drink")]
    //    FoodAndBeverage = BusinessSearchCategory.FoodAndBeverage,

    //    [Display(Name = "Services - General")]
    //    GeneralServices = BusinessSearchCategory.GeneralServices,

    //    [Display(Name = "Services - Home")]
    //    HomeServices = BusinessSearchCategory.HomeServices,

    //    [Display(Name = "Services - Human")]
    //    HumanServices = BusinessSearchCategory.HumanServices,

    //    [Display(Name = "Services - Public")]
    //    PublicServices = BusinessSearchCategory.PublicServices,

    //    [Display(Name = "Sports & Fitness")]
    //    SportFitness = BusinessSearchCategory.SportFitness,

    //    [Display(Name = "Support & Information")]
    //    SupportInformation = BusinessSearchCategory.SupportInformation,
    //}
}
