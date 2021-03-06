﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Looking2.Web.Domain;
using Looking2.Web.DataAccess;
using Looking2.Web.Services;
using System.Globalization;

namespace Looking2.Web
{
    public static class Seed
    {
        public static void SeedCategories(ICategoriesRepository _categoriesRepo)
        {
            _categoriesRepo.Db.DropCollection("categories");
            var newCategories = GetSeedCategories();
            foreach (var item in newCategories)
            {
                _categoriesRepo.Add(item);
            }
        }

        public static void SeedBusinesses(IBusinessRepository _businessRepo)
        {
            var businesses = GetSeedBusinessListings();
            // TODO: Finish this with find and replace functionality
            foreach (var business in businesses)
            {
                _businessRepo.Add(business);
            }
        }

        public static void SeedEvents(IEventsRepository _eventRepo)
        {
            var events = GetSeedEventListings();
            foreach (var e in events)
            {
                _eventRepo.Add(e);
            }
        }

        public static void SeedForms(IBusinessFormsRepo _businessFormsRepo, IEventFormsRepo _eventFormsRepo)
        {
            //var newForms = GetSeedEventForms();
            //foreach (var item in newForms)
            //{
            //    var existingForm = _eventFormsRepo.Find(f => f.FormName == item.FormName).ToList();

            //    if (existingForm.Count < 1 )
            //    {
            //        _eventFormsRepo.Add(item);
            //    }
            //}

            var newBusinessForms = GetSeedBusinessForms();
            foreach (var item in newBusinessForms)
            {
                var existingForm = _businessFormsRepo.Find(f => f.FormName == item.FormName).ToList();

                if (existingForm.Count < 1)
                {
                    _businessFormsRepo.Add(item);
                }
            }
        }

        public static void SeedAll(ICategoriesRepository _categoriesRepo, 
            IBusinessFormsRepo _businessFormsRepo, 
            IEventFormsRepo _eventFormsRepo,
            IEventsRepository _eventRepo,
            IBusinessRepository _businessRepo)
        {
            SeedCategories(_categoriesRepo);
            SeedForms(_businessFormsRepo, _eventFormsRepo);
            SeedBusinesses(_businessRepo);
            SeedEvents(_eventRepo);
        }

        public static void DropListingCollections(
            ICategoriesRepository _categoriesRepo,
            IEventsRepository _eventRepo,
            IBusinessRepository _businessRepo)
        {
            _businessRepo.Db.DropCollection("businesses");
            _eventRepo.Db.DropCollection("events");
            //_categoriesRepo.Db.DropCollection("categories");

        }

        public static void DropFormCollections(IEventFormsRepo _eventForms, IBusinessFormsRepo _businessForms)
        {
            _eventForms.Db.DropCollection("eventforms");
            _businessForms.Db.DropCollection("businessforms");
        }
        
        private static List<Category> GetSeedCategories()
        {
            var seedRepo = new SeedRepository();
            return seedRepo.GetSeedCategories();
            //var result = new List<Category>()
            //{
            //    new Category
            //    {
            //        Name = "Benefit",
            //        DisplayName = "Fundraisers & Benefits",
            //        Description ="when it's all about helping a good cause",
            //        Type = ListingCategory.Event,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name = "Gig",
            //        DisplayName = "Gigs",
            //        Description ="for local bands playing in local venues",
            //        Type = ListingCategory.Event,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name="ArtistIndividual",
            //        DisplayName = "Individual Artists",
            //        Description ="for individual performers appearing alone or together",
            //        Type = ListingCategory.Event,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name = "ArtistMultiple",
            //        DisplayName = "Multiple Artists",
            //        Description ="covers opening acts, joint appearances and/or special guests",
            //        Type = ListingCategory.Event,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name="Series",
            //        DisplayName = "Series",
            //        Description ="when the event is part of a series",
            //        Type = ListingCategory.Event,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name = "Exhibit",
            //        DisplayName = "Exhibits",
            //        Description ="Is your work going to be on display somewhere?  choose this one!",
            //        Type = ListingCategory.Event,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name = "Concert",
            //        DisplayName = "Concert Tours",
            //        Description ="for any show on tour: concerts, comedians, speakers, whatever",
            //        Type = ListingCategory.Event,
            //        Active = true
            //    },
            //    new Category
            //    {

            //        Name = "Orchestra",
            //        DisplayName = "Troupes, Companies, Orchestras",
            //        Description ="when who's putting on the show is as important as the show they're putting on",
            //        Type = ListingCategory.Event,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name="Other",
            //        DisplayName = "Other",
            //        Description ="any event that does not fall under any of the other categories, use this one!",
            //        Type = ListingCategory.Event,
            //        Active = true
            //    },
            //    // Business Categories
            //    new Category
            //    {
            //        Name="Artists",
            //        DisplayName = "Artists, Artisans & Musicians",
            //        Description ="If you create things of beauty, pick this one",
            //        Type = ListingCategory.Business,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name="HealthCare",
            //        DisplayName = "Health Care",
            //        Description ="For those who practice the healing arts and sciences (& have a degree AND/OR state certification)",
            //        Type = ListingCategory.Business,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name="AltHealthCare",
            //        DisplayName = "Alternative Health Care",
            //        Description ="For those with a non-traditional approach to addressing our aches and pains",
            //        Type = ListingCategory.Business,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name="Information",
            //        DisplayName = "Information",
            //        Description ="Do you offer information of any kind?",
            //        Type = ListingCategory.Business,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name="Instruction",
            //        DisplayName = "Instruction",
            //        Description ="Lessons and instruction of all kinds",
            //        Type = ListingCategory.Business,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name="Lawyers",
            //        DisplayName = "Lawyers",
            //        Description ="For practitioners of one of the world's oldest professions",
            //        Type = ListingCategory.Business,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name = "Restaurant",
            //        DisplayName = "Places to Eat",
            //        Description ="Do you serve prepared food, ready to eat right now?",
            //        Type = ListingCategory.Business,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name="ServiceProviders",
            //        DisplayName = "Service Providers",
            //        Description ="For those who provide services people cannot or will not do for themselves (from walking dogs to building houses)",
            //        Type = ListingCategory.Business,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name="Shopkeepers",
            //        DisplayName = "Shopkeepers",
            //        Description ="If you sell (or rent out) stuff for a livelihood, choose this one, even if it's from out of your kitchen",
            //        Type = ListingCategory.Business,
            //        Active = true
            //    },
            //    new Category
            //    {
            //        Name="Support",
            //        DisplayName = "Support",
            //        Description ="Do you offer support of any kind?",
            //        Type = ListingCategory.Business,
            //        Active = false
            //    },
            //    new Category
            //    {
            //        Name="Other",
            //        DisplayName = "Other",
            //        Description ="any business that does not fall under the other categories, use this one!",
            //        Type = ListingCategory.Business,
            //        Active = true
            //    }


            //};
            //int displayCount = 1;
            //foreach (var item in result)
            //{
            //    item.DisplayOrder = displayCount;
            //    displayCount++;
            //}
            //return result;

        }

        private static List<EventFormData> GetSeedEventForms()
        {
            return new List<EventFormData>()
            {
                new EventFormData
                {
                     FormName = "GigCreate",
                     //Category = EventDescription.LiveMusic,
                     Type = EventType.Gig,
                     Title0Label = "What is the name of the band?",
                     Title1Label = "Where are you playing?",
                     Description0Label = "What genre music will be played?",
                     Description1Label = "In this field, you should enter if it's one night only or a regular gig ('every Friday night')",
                     Description2Label = "And now the time:",
                     Description3Label = "",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "Your web presence (if any):",
                     Contact1Label = "The venue's website:",
                     Contact2Label = "",
                     Contact3Label = "",
                     Contact4Label = "",
                     PriceLabel = "Cost (type FREE! if it's free)",
                     AdmissionInfoLabel = "Ticket info:",
                     DateLabel = "If you want to highlight a specific date, enter it here:",
                     BragLabel = "Tell us why we should come:",
                     PageTitle = "Gig Listing",
                     HeadingPartial = "_GigHeading"
                },
                new EventFormData
                {
                     FormName = "BenefitCreate",
                     //Category = EventDescription.Other,
                     Type = EventType.Benefit,
                     Title0Label = "What is the name of the event?",
                     Title1Label = "Where is it being held (name of venue)?",
                     Description0Label = "Who does it benefit?",
                     Description1Label = "In a word or two, what kind of an event is it?",
                     Description2Label = "What ages is it for?",
                     Description3Label = "Please enter the date range for the event or if it's one day or one night only:",
                     Description4Label = "And now the time:",
                     Description5Label = "Please send donations to:",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "Website or phone number to contact for information",
                     Contact1Label = "The venue's website:",
                     Contact2Label = "",
                     Contact3Label = "",
                     Contact4Label = "",
                     PriceLabel = "Please provide ticket/cost info here",
                     AdmissionInfoLabel = "",
                     DateLabel = "If you want to highlight a specific date, enter it here",
                     BragLabel = "",
                     PageTitle = "Benefit Listing",
                     HeadingPartial = "_BenefitHeading"
                },
                new EventFormData
                {
                     FormName = "ArtistIndividualCreate",
                     //Category = EventDescription.LiveMusic,
                     Type = EventType.ArtistIndividual,
                     Title0Label = "",
                     Title1Label = "?",
                     Description0Label = "",
                     Description1Label = "",
                     Description2Label = "",
                     Description3Label = "",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "",
                     Contact1Label = "",
                     Contact2Label = "",
                     Contact3Label = "",
                     Contact4Label = "",
                     PriceLabel = "",
                     AdmissionInfoLabel = "",
                     DateLabel = "",
                     BragLabel = "",
                     PageTitle = "",
                     HeadingPartial = "_ArtistIndividualHeading"
                },
                new EventFormData
                {
                     FormName = "ArtistMultipleCreate",
                     //Category = EventDescription.LiveMusic,
                     Type = EventType.ArtistMultiple,
                     Title0Label = "",
                     Title1Label = "?",
                     Description0Label = "",
                     Description1Label = "",
                     Description2Label = "",
                     Description3Label = "",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "",
                     Contact1Label = "",
                     Contact2Label = "",
                     Contact3Label = "",
                     Contact4Label = "",
                     PriceLabel = "",
                     AdmissionInfoLabel = "",
                     DateLabel = "",
                     BragLabel = "",
                     PageTitle = "",
                     HeadingPartial = "_ArtistMultipleHeading"
                },
                new EventFormData
                {
                     FormName = "SeriesCreate",
                     //Category = EventDescription.Other,
                     Type = EventType.Series,
                     Title0Label = "",
                     Title1Label = "?",
                     Description0Label = "",
                     Description1Label = "",
                     Description2Label = "",
                     Description3Label = "",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "",
                     Contact1Label = "",
                     Contact2Label = "",
                     Contact3Label = "",
                     Contact4Label = "",
                     PriceLabel = "",
                     AdmissionInfoLabel = "",
                     DateLabel = "",
                     BragLabel = "",
                     PageTitle = "",
                     HeadingPartial = "_SeriesHeading"
                },
                new EventFormData
                {
                     FormName = "ExhibitCreate",
                     //Category = EventDescription.Other,
                     Type = EventType.Exhibit,
                     Title0Label = "",
                     Title1Label = "?",
                     Description0Label = "",
                     Description1Label = "",
                     Description2Label = "",
                     Description3Label = "",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "",
                     Contact1Label = "",
                     Contact2Label = "",
                     Contact3Label = "",
                     Contact4Label = "",
                     PriceLabel = "",
                     AdmissionInfoLabel = "",
                     DateLabel = "",
                     BragLabel = "",
                     PageTitle = "",
                     HeadingPartial = "_ExhibitHeading"
                },
                new EventFormData
                {
                     FormName = "ConcertCreate",
                     //Category = EventDescription.LiveMusic,
                     Type = EventType.Concert,
                     Title0Label = "",
                     Title1Label = "?",
                     Description0Label = "",
                     Description1Label = "",
                     Description2Label = "",
                     Description3Label = "",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "",
                     Contact1Label = "",
                     Contact2Label = "",
                     Contact3Label = "",
                     Contact4Label = "",
                     PriceLabel = "",
                     AdmissionInfoLabel = "",
                     DateLabel = "",
                     BragLabel = "",
                     PageTitle = "",
                     HeadingPartial = "_ConcertHeading"
                },
                new EventFormData
                {
                     FormName = "OrchestraCreate",
                     //Category = EventDescription.LiveMusic,
                     Type = EventType.Orchestra,
                     Title0Label = "",
                     Title1Label = "?",
                     Description0Label = "",
                     Description1Label = "",
                     Description2Label = "",
                     Description3Label = "",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "",
                     Contact1Label = "",
                     Contact2Label = "",
                     Contact3Label = "",
                     Contact4Label = "",
                     PriceLabel = "",
                     AdmissionInfoLabel = "",
                     DateLabel = "",
                     BragLabel = "",
                     PageTitle = "",
                     HeadingPartial = "_OrchestraHeading"
                },
                new EventFormData
                {
                     FormName = "OtherCreate",
                     //Category = EventDescription.Other,
                     Type = EventType.Other,
                     Title0Label = "",
                     Title1Label = "?",
                     Description0Label = "",
                     Description1Label = "",
                     Description2Label = "",
                     Description3Label = "",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "",
                     Contact1Label = "",
                     Contact2Label = "",
                     Contact3Label = "",
                     Contact4Label = "",
                     PriceLabel = "",
                     AdmissionInfoLabel = "",
                     DateLabel = "",
                     BragLabel = "",
                     PageTitle = "",
                     HeadingPartial = "_OtherHeading"
                },
            };
        }

        private static List<BusinessFormData> GetSeedBusinessForms()
        {
            return new List<BusinessFormData>()
            {
                //new BusinessFormData
                //{
                //    FormName = "ArtistsCreate",
                //     //SearchDescription = EventDescription.Other,
                //     BusinessType = BusinessType.Artists,
                //     Title0Label = "",
                //     Title1Label = "",
                //     Description0Label = "",
                //     Description1Label = "",
                //     Description2Label = "",
                //     Description3Label = "",
                //     Description4Label = "",
                //     Description5Label = "",
                //     Description6Label = "",
                //     Description7Label = "",
                //     Description8Label = "",
                //     Description9Label = "",
                //     Contact0Label = "",
                //     Contact1Label = "",
                //     Contact2Label = "",
                //     Contact3Label = "",
                //     Contact4Label = "",
                //     BragLabel = "",
                //     PageTitle = "",
                //     HeadingPartial = "_ArtistsHeading"
                //},
                //new BusinessFormData
                //{
                //    FormName = "HealthCareCreate",
                //     //SearchDescription = EventDescription.Other,
                //     BusinessType = BusinessType.HealthCare,
                //     Title0Label = "What is the name of your practice?",
                //     Title1Label = "",
                //     Description0Label = "What is your role as an HCP?",
                //     Description1Label = "Please enter your last name:",
                //     Description2Label = "Please enter your first name and middle intial:",
                //     Description3Label = "Please enter your degree(s):",
                //     Description4Label = "What field of medicine/health care do you focus on?",
                //     Description5Label = "Enter conditions treated or treatments offered here:",
                //     Description6Label = "",
                //     Description7Label = "",
                //     Description8Label = "",
                //     Description9Label = "",
                //     Contact0Label = "Your practice's website:",
                //     Contact1Label = "Please enter up to 2 phone numbers:",
                //     Contact2Label = "Please enter your email if you want:",
                //     Contact3Label = "",
                //     Contact4Label = "",
                //     BragLabel = "Brag a little(certifications, affiliated hospitals, etc)",
                //     PageTitle = "",
                //     HeadingPartial = "_HealthCareHeading"
                //},
                //new BusinessFormData
                //{
                //    FormName = "AltHealthCareCreate",
                //     //SearchDescription = EventDescription.Other,
                //     BusinessType = BusinessType.AltHealthCare,
                //     Title0Label = "What is the name of your practice?",
                //     Title1Label = "",
                //     Description0Label = "What is your role as an HCP?",
                //     Description1Label = "Please enter your last name:",
                //     Description2Label = "Please enter your first name and middle intial:",
                //     Description3Label = "What conditions do you focus on?",
                //     Description4Label = "",
                //     Description5Label = "",
                //     Description6Label = "",
                //     Description7Label = "",
                //     Description8Label = "",
                //     Description9Label = "",
                //     Contact0Label = "Your practice's website:",
                //     Contact1Label = "Please enter up to 2 phone numbers:",
                //     Contact2Label = "Please enter your email if you want:",
                //     Contact3Label = "",
                //     Contact4Label = "",
                //     BragLabel = "State your mission here:",
                //     PageTitle = "",
                //     HeadingPartial = "_AltHealthCareHeading"
                //},
                //new BusinessFormData
                //{
                //    FormName = "InformationCreate",
                //     //SearchDescription = EventDescription.Other,
                //     BusinessType = BusinessType.Information,
                //     Title0Label = "What is the name of your business?",
                //     Title1Label = "",
                //     Description0Label = "Broad categorization:",
                //     Description1Label = "General Description:",
                //     Description2Label = "Specifics:",
                //     Description3Label = "",
                //     Description4Label = "",
                //     Description5Label = "",
                //     Description6Label = "",
                //     Description7Label = "",
                //     Description8Label = "",
                //     Description9Label = "",
                //     Contact0Label = "What is your website?",
                //     Contact1Label = "Phone (optional):",
                //     Contact2Label = "Email (optional):",
                //     Contact3Label = "",
                //     Contact4Label = "",
                //     BragLabel = "Brag a little",
                //     PageTitle = "",
                //     HeadingPartial = "_InformationHeading"
                //},
                //new BusinessFormData
                //{
                //    FormName = "InstructionCreate",
                //     //SearchDescription = EventDescription.Other,
                //     BusinessType = BusinessType.Instruction,
                //     Title0Label = "What name do you go by as an instructor?",
                //     Title1Label = "",
                //     Description0Label = "What kind of instruction do you offer?",
                //     Description1Label = "What age group do you work with?",
                //     Description2Label = "What skill levels?",
                //     Description3Label = "",
                //     Description4Label = "",
                //     Description5Label = "",
                //     Description6Label = "",
                //     Description7Label = "",
                //     Description8Label = "",
                //     Description9Label = "",
                //     Contact0Label = "What is your website?",
                //     Contact1Label = "Phone (optional):",
                //     Contact2Label = "Email (optional):",
                //     Contact3Label = "",
                //     Contact4Label = "",
                //     BragLabel = "Brag a little",
                //     PageTitle = "",
                //     HeadingPartial = "_InstructionHeading"
                //},
                //new BusinessFormData
                //{
                //    FormName = "LawyersCreate",
                //     //SearchDescription = EventDescription.Other,
                //     BusinessType = BusinessType.Lawyers,
                //     Title0Label = "What is the name of your law firm?",
                //     Title1Label = "",
                //     Description0Label = "Please enter your last name:",
                //     Description1Label = "Please enter your first name and middle initial:",
                //     Description2Label = "In which states are you licensed to practice?",
                //     Description3Label = "What field of law is this listing for?",
                //     Description4Label = "What 'buzz words' would you put in an ad?",
                //     Description5Label = "",
                //     Description6Label = "",
                //     Description7Label = "",
                //     Description8Label = "",
                //     Description9Label = "",
                //     Contact0Label = "What is your website?",
                //     Contact1Label = "Phone (optional):",
                //     Contact2Label = "Email (optional):",
                //     Contact3Label = "",
                //     Contact4Label = "",
                //     BragLabel = "Brag a little",
                //     PageTitle = "",
                //     HeadingPartial = "_LawyersHeading"
                //},
                //new BusinessFormData
                //{
                //    FormName = "RestaurantCreate",
                //     //SearchDescription = EventDescription.Other,
                //     BusinessType = BusinessType.Restaurant,
                //     Title0Label = "What is the name of your eatery?",
                //     Title1Label = "",
                //     Description0Label = "What type of eatery is it?",
                //     Description1Label = "What meals do you serve?",
                //     Description2Label = "Popular menu items or your cuisine type?",
                //     Description3Label = "Pricing?",
                //     Description4Label = "",
                //     Description5Label = "",
                //     Description6Label = "",
                //     Description7Label = "",
                //     Description8Label = "",
                //     Description9Label = "",
                //     Contact0Label = "What is your website?",
                //     Contact1Label = "Phone (optional):",
                //     Contact2Label = "Email (optional):",
                //     Contact3Label = "",
                //     Contact4Label = "",
                //     BragLabel = "Brag a little",
                //     PageTitle = "",
                //     HeadingPartial = "_RestaurantHeading"
                //},
                //new BusinessFormData
                //{
                //    FormName = "ServiceProvidersCreate",
                //     //SearchDescription = EventDescription.Other,
                //     BusinessType = BusinessType.ServiceProviders,
                //     Title0Label = "",
                //     Title1Label = "",
                //     Description0Label = "",
                //     Description1Label = "",
                //     Description2Label = "",
                //     Description3Label = "",
                //     Description4Label = "",
                //     Description5Label = "",
                //     Description6Label = "",
                //     Description7Label = "",
                //     Description8Label = "",
                //     Description9Label = "",
                //     Contact0Label = "",
                //     Contact1Label = "",
                //     Contact2Label = "",
                //     Contact3Label = "",
                //     Contact4Label = "",
                //     BragLabel = "",
                //     PageTitle = "",
                //     HeadingPartial = "_ServiceProvidersHeading"
                //},
                //new BusinessFormData
                //{
                //    FormName = "ShopkeepersCreate",
                //     //SearchDescription = EventDescription.Other,
                //     BusinessType = BusinessType.Shopkeepers,
                //     Title0Label = "",
                //     Title1Label = "",
                //     Description0Label = "",
                //     Description1Label = "",
                //     Description2Label = "",
                //     Description3Label = "",
                //     Description4Label = "",
                //     Description5Label = "",
                //     Description6Label = "",
                //     Description7Label = "",
                //     Description8Label = "",
                //     Description9Label = "",
                //     Contact0Label = "",
                //     Contact1Label = "",
                //     Contact2Label = "",
                //     Contact3Label = "",
                //     Contact4Label = "",
                //     BragLabel = "",
                //     PageTitle = "",
                //     HeadingPartial = "_ShopkeepersHeading"
                //},
                //new BusinessFormData
                //{
                //    FormName = "SupportCreate",
                //     //SearchDescription = EventDescription.Other,
                //     BusinessType = BusinessType.Support,
                //     Title0Label = "",
                //     Title1Label = "",
                //     Description0Label = "",
                //     Description1Label = "",
                //     Description2Label = "",
                //     Description3Label = "",
                //     Description4Label = "",
                //     Description5Label = "",
                //     Description6Label = "",
                //     Description7Label = "",
                //     Description8Label = "",
                //     Description9Label = "",
                //     Contact0Label = "",
                //     Contact1Label = "",
                //     Contact2Label = "",
                //     Contact3Label = "",
                //     Contact4Label = "",
                //     BragLabel = "",
                //     PageTitle = "",
                //     HeadingPartial = "_SupportHeading"
                //},
                new BusinessFormData
                {
                    FormName = "OtherCreate",
                     //SearchDescription = EventDescription.Other,
                     BusinessType = BusinessType.Other,
                     Title0Label = "",
                     Title1Label = "",
                     Description0Label = "",
                     Description1Label = "",
                     Description2Label = "",
                     Description3Label = "",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "",
                     Contact1Label = "",
                     Contact2Label = "",
                     Contact3Label = "",
                     Contact4Label = "",
                     BragLabel = "",
                     PageTitle = "",
                     HeadingPartial = "_OtherHeading"
                },
            };
        }

        private static List<BusinessListing> GetSeedBusinessListings()
        {
            var result = new List<BusinessListing>();

            var bondHealthCare = new BusinessListing();
            bondHealthCare.Titles.Add("Bond Physical Therapy");
            bondHealthCare.Descriptions.Add("Physical Therapy");
            bondHealthCare.Descriptions.Add("Bond");
            bondHealthCare.Descriptions.Add("John P");
            bondHealthCare.Descriptions.Add("DPT");
            bondHealthCare.Descriptions.Add("general physical therapy");
            bondHealthCare.Descriptions.Add("All PT needs");
            bondHealthCare.Contact.Add("www.bondpt.com");
            bondHealthCare.Contact.Add("777.777.7777, 724.944.1234");
            bondHealthCare.Brag = "We are dedicated to excellence and you will get the best experience from us.";
            bondHealthCare.BusinessType = BusinessType.HealthCare;
            result.Add(bondHealthCare);

            return result;
        }

        private static List<EventListing> GetSeedEventListings()
        {
            var result = new List<EventListing>();
            var bondGig = new EventListing();
            bondGig.Titles.Add("Bond Rockers");
            bondGig.Venue = "Pepsi Center";
            bondGig.Descriptions.Add("rock & roll");
            bondGig.Descriptions.Add("one night only");
            bondGig.Descriptions.Add("doors open at 7:30pm");
            bondGig.Contact.Add("www.bondrockers.com");
            bondGig.Contact.Add("www.pepsicenter.com");
            bondGig.AdmissionInfo = "www.ticketmaster.com";
            bondGig.Brag = "Best rock show in town";
            bondGig.Date = DateTime.Now.AddDays(10).Date;
            bondGig.EventType = EventType.Gig;
            bondGig.Price = "FREE!";
            bondGig.LongDescription = "We put on the best rock show you will ever see. Come to hear the face melting guitar solos of new and classic rock hits!";
            bondGig.Location = new List<string>() { "CO", "Denver", "Denver", "HiLo", "101 E Colfax", "Downtown", "By the river" };
            result.Add(bondGig);

            var gig2 = new EventListing();
            gig2.Titles.Add("Jake Miller plus guests");
            gig2.Venue = "Rex Theater";
            gig2.Descriptions.Add("pop rap");
            gig2.Descriptions.Add("one night only");
            gig2.Descriptions.Add("8 pm");
            gig2.Contact.Add("jakemiller.com");
            gig2.Contact.Add("greyandaprod.com");
            gig2.AdmissionInfo = "tickets at Druskyent.com or call 877-4-FLY-Tix";
            gig2.Brag = "Newest album: 2:00 AM inLA";
            gig2.Date = DateTime.Parse("09/16/2017");
            gig2.EventType = EventType.Gig;
            result.Add(gig2);

            var gig3 = new EventListing();
            gig3.Titles.Add("3Teeth with Morpheus Laughing & God Hates Unicorns");
            gig3.Venue = "Rex Theater";
            gig3.Descriptions.Add("eclectic");
            gig3.Descriptions.Add("one night only");
            gig3.Descriptions.Add("7:30 pm");
            gig3.Contact.Add("greyareaprod.com");
            gig3.AdmissionInfo = "tickets at Druskyent.com or call 877-4-FLY-Tix";
            gig3.Date = DateTime.Parse("07/15/2017");
            gig3.EventType = EventType.Gig;
            result.Add(gig3);

            var gig4 = new EventListing();
            gig4.Titles.Add("Shelf Life String Band");
            gig4.Venue = "The Park House";
            gig4.Descriptions.Add("bluegrass & new grass");
            gig4.Descriptions.Add("every Wednesday night");
            gig4.Descriptions.Add("around 8 pm");
            gig4.Contact.Add("shelflifestringband.com");
            gig4.Contact.Add("parkhousepgh.com");
            gig4.Price = "FREE!";
            gig4.Brag = "'this ain't your pappy's bluegrass'";
            gig4.Date = DateTime.Parse("07/26/2017");
            gig4.EventType = EventType.Gig;
            gig4.Location = new List<string>() { "PA", "Allegheny", "Pittsburgh", "North Side", "405 East Ohio Street", "north", "just of Cedar Ave by the park" };
            result.Add(gig4);

            var gig5 = new EventListing();
            gig5.Titles.Add("Bond Unplugged");
            gig5.Venue = "Red Rocks";
            gig5.Descriptions.Add("classic rock");
            gig5.Descriptions.Add("one night only");
            gig5.Descriptions.Add("doors open at 6:15");
            gig5.Contact.Add("www.bondmusic.com");
            gig5.Contact.Add("www.redrocks.com");
            gig5.Price = "$50";
            gig5.Brag = "best classic rock covers";
            gig5.Date = DateTime.Parse("08/15/2017");
            gig5.EventType = EventType.Gig;
            gig5.Location = new List<string>() { "CO", "Jefferson", "Denver", "Morrison", "101 Main Street", "West", "just off E-470" };
            result.Add(gig5);

            return result;
        }
    }
}
