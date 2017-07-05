using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Looking2.Web.Domain;
using Looking2.Web.DataAccess;

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
                // Check for existing to determine insert or update
                var existingCategory = _categoriesRepo.Find(c => c.Name == item.Name).ToList();
                // Insert new
                if (existingCategory.Count < 1)
                {
                    _categoriesRepo.Add(item);
                }
                // Update
                else
                {
                    item.Id = existingCategory[0].Id;
                    var result = _categoriesRepo.Collection.FindOneAndReplace<Category>(c => c.Name == item.Name, item);
                }

            }
        }

        public static void SeedForms(IBusinessFormsRepo _businessFormsRepo, IEventFormsRepo _eventFormsRepo)
        {
            var newForms = GetSeedEventForms();
            foreach (var item in newForms)
            {
                var existingForm = _eventFormsRepo.Find(f => f.FormName == item.FormName).ToList();

                if (existingForm.Count < 1 )
                {
                    _eventFormsRepo.Add(item);
                }
                else
                {
                    item.Id = existingForm[0].Id;
                    var result = _eventFormsRepo.Collection.FindOneAndReplace<EventFormData>(f => f.FormName == item.FormName, item);
                }
            }

            var newBusinessForms = GetSeedBusinessForms();
            foreach (var item in newBusinessForms)
            {
                var existingForm = _businessFormsRepo.Find(f => f.FormName == item.FormName).ToList();

                if (existingForm.Count < 1)
                {
                    _businessFormsRepo.Add(item);
                }
                else
                {
                    item.Id = existingForm[0].Id;
                    var result = _businessFormsRepo.Collection.FindOneAndReplace<BusinessFormData>(f => f.FormName == item.FormName, item);
                }
            }
        }

        public static void SeedAll(ICategoriesRepository _categoriesRepo, IBusinessFormsRepo _businessFormsRepo, IEventFormsRepo _eventFormsRepo)
        {
            SeedCategories(_categoriesRepo);
            SeedForms(_businessFormsRepo, _eventFormsRepo);
        }

        private static List<Category> GetSeedCategories()
        {
            var result = new List<Category>()
            {
                new Category
                {
                    Name = "Benefit",
                    DisplayName = "Fundraisers & Benefits",
                    Description ="when it's all about helping a good cause",
                    Type = ListingCategory.Event,
                    Active = true
                },
                new Category
                {
                    Name = "Gig",
                    DisplayName = "Gigs",
                    Description ="for local bands playing in local venues",
                    Type = ListingCategory.Event,
                    Active = true
                },
                new Category
                {
                    Name="ArtistIndividual",
                    DisplayName = "Individual Artists",
                    Description ="for individual performers appearing alone or together",
                    Type = ListingCategory.Event,
                    Active = true
                },
                new Category
                {
                    Name = "ArtistMultiple",
                    DisplayName = "Multiple Artists",
                    Description ="covers opening acts, joint appearances and/or special guests",
                    Type = ListingCategory.Event,
                    Active = true
                },
                new Category
                {
                    Name="Series",
                    DisplayName = "Series",
                    Description ="when the event is part of a series",
                    Type = ListingCategory.Event,
                    Active = true
                },
                new Category
                {
                    Name = "Exhibit",
                    DisplayName = "Exhibits",
                    Description ="Is your work going to be on display somewhere?  choose this one!",
                    Type = ListingCategory.Event,
                    Active = true
                },
                new Category
                {
                    Name = "Concert",
                    DisplayName = "Concert Tours",
                    Description ="for any show on tour: concerts, comedians, speakers, whatever",
                    Type = ListingCategory.Event,
                    Active = true
                },
                new Category
                {

                    Name = "Orchestra",
                    DisplayName = "Troupes, Companies, Orchestras",
                    Description ="when who's putting on the show is as important as the show they're putting on",
                    Type = ListingCategory.Event,
                    Active = true
                },
                new Category
                {
                    Name="Other",
                    DisplayName = "Other",
                    Description ="any event that does not fall under any of the other categories, use this one!",
                    Type = ListingCategory.Event,
                    Active = true
                },
                // Business Categories
                new Category
                {
                    Name="Artists",
                    DisplayName = "Artists, Artisans & Musicians",
                    Description ="If you create things of beauty, pick this one",
                    Type = ListingCategory.Business,
                    Active = true
                },
                new Category
                {
                    Name="HealthCare",
                    DisplayName = "Health Care",
                    Description ="For those who practice the healing arts and sciences (& have a degree AND/OR state certification)",
                    Type = ListingCategory.Business,
                    Active = true
                },
                new Category
                {
                    Name="AltHealthCare",
                    DisplayName = "Alternative Health Care",
                    Description ="For those with a non-traditional approach to addressing our aches and pains",
                    Type = ListingCategory.Business,
                    Active = true
                },
                new Category
                {
                    Name="Information",
                    DisplayName = "Information",
                    Description ="Do you offer information of any kind?",
                    Type = ListingCategory.Business,
                    Active = true
                },
                new Category
                {
                    Name="Instruction",
                    DisplayName = "Instruction",
                    Description ="Lessons and instruction of all kinds",
                    Type = ListingCategory.Business,
                    Active = true
                },
                new Category
                {
                    Name="Lawyers",
                    DisplayName = "Lawyers",
                    Description ="For practitioners of one of the world's oldest professions",
                    Type = ListingCategory.Business,
                    Active = true
                },
                new Category
                {
                    Name = "Restaurant",
                    DisplayName = "Places to Eat",
                    Description ="Do you serve prepared food, ready to eat right now?",
                    Type = ListingCategory.Business,
                    Active = true
                },
                new Category
                {
                    Name="ServiceProviders",
                    DisplayName = "Service Providers",
                    Description ="For those who provide services people cannot or will not do for themselves (from walking dogs to building houses)",
                    Type = ListingCategory.Business,
                    Active = false
                },
                new Category
                {
                    Name="Shopkeepers",
                    DisplayName = "Shopkeepers",
                    Description ="If you sell (or rent out) stuff for a livelihood, choose this one, even if it's from out of your kitchen",
                    Type = ListingCategory.Business,
                    Active = false
                },
                new Category
                {
                    Name="Support",
                    DisplayName = "Support",
                    Description ="Do you offer support of any kind?",
                    Type = ListingCategory.Business,
                    Active = false
                }


            };
            int displayCount = 1;
            foreach (var item in result)
            {
                item.DisplayOrder = displayCount;
                displayCount++;
            }
            return result;

        }

        private static List<EventFormData> GetSeedEventForms()
        {
            return new List<EventFormData>()
            {
                new EventFormData
                {
                     FormName = "GigCreate",
                     Category = EventDescription.LiveMusic,
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
                     Category = EventDescription.Other,
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
                     Category = EventDescription.LiveMusic,
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
                     Category = EventDescription.LiveMusic,
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
                     Category = EventDescription.Other,
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
                     Category = EventDescription.Other,
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
                     Category = EventDescription.LiveMusic,
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
                     Category = EventDescription.LiveMusic,
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
                     Category = EventDescription.Other,
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
                new BusinessFormData
                {
                    FormName = "ArtistsCreate",
                     SearchDescription = EventDescription.Other,
                     BusinessType = BusinessType.Artists,
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
                     HeadingPartial = "_ArtistsHeading"
                },
                new BusinessFormData
                {
                    FormName = "HealthCareCreate",
                     SearchDescription = EventDescription.Other,
                     BusinessType = BusinessType.HealthCare,
                     Title0Label = "What is the name of your practice?",
                     Title1Label = "",
                     Description0Label = "What is your role as an HCP?",
                     Description1Label = "Please enter your last name:",
                     Description2Label = "Please enter your first name and middle intial:",
                     Description3Label = "Please enter your degree(s):",
                     Description4Label = "What field of medicine/health care do you focus on?",
                     Description5Label = "Enter conditions treated or treatments offered here:",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "Your practice's website:",
                     Contact1Label = "Please enter up to 2 phone numbers:",
                     Contact2Label = "Please enter your email if you want:",
                     Contact3Label = "",
                     Contact4Label = "",
                     BragLabel = "Brag a little(certifications, affiliated hospitals, etc)",
                     PageTitle = "",
                     HeadingPartial = "_HealthCareHeading"
                },
                new BusinessFormData
                {
                    FormName = "AltHealthCareCreate",
                     SearchDescription = EventDescription.Other,
                     BusinessType = BusinessType.AltHealthCare,
                     Title0Label = "What is the name of your practice?",
                     Title1Label = "",
                     Description0Label = "What is your role as an HCP?",
                     Description1Label = "Please enter your last name:",
                     Description2Label = "Please enter your first name and middle intial:",
                     Description3Label = "What conditions do you focus on?",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "Your practice's website:",
                     Contact1Label = "Please enter up to 2 phone numbers:",
                     Contact2Label = "Please enter your email if you want:",
                     Contact3Label = "",
                     Contact4Label = "",
                     BragLabel = "State your mission here:",
                     PageTitle = "",
                     HeadingPartial = "_AltHealthCareHeading"
                },
                new BusinessFormData
                {
                    FormName = "InformationCreate",
                     SearchDescription = EventDescription.Other,
                     BusinessType = BusinessType.Information,
                     Title0Label = "What is the name of your business?",
                     Title1Label = "",
                     Description0Label = "Broad categorization:",
                     Description1Label = "General Description:",
                     Description2Label = "Specifics:",
                     Description3Label = "",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "What is your website?",
                     Contact1Label = "Phone (optional):",
                     Contact2Label = "Email (optional):",
                     Contact3Label = "",
                     Contact4Label = "",
                     BragLabel = "Brag a little",
                     PageTitle = "",
                     HeadingPartial = "_InformationHeading"
                },
                new BusinessFormData
                {
                    FormName = "InstructionCreate",
                     SearchDescription = EventDescription.Other,
                     BusinessType = BusinessType.Instruction,
                     Title0Label = "What name do you go by as an instructor?",
                     Title1Label = "",
                     Description0Label = "What kind of instruction do you offer?",
                     Description1Label = "What age group do you work with?",
                     Description2Label = "What skill levels?",
                     Description3Label = "",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "What is your website?",
                     Contact1Label = "Phone (optional):",
                     Contact2Label = "Email (optional):",
                     Contact3Label = "",
                     Contact4Label = "",
                     BragLabel = "Brag a little",
                     PageTitle = "",
                     HeadingPartial = "_InstructionHeading"
                },
                new BusinessFormData
                {
                    FormName = "LawyersCreate",
                     SearchDescription = EventDescription.Other,
                     BusinessType = BusinessType.Lawyers,
                     Title0Label = "What is the name of your law firm?",
                     Title1Label = "",
                     Description0Label = "Please enter your last name:",
                     Description1Label = "Please enter your first name and middle initial:",
                     Description2Label = "In which states are you licensed to practice?",
                     Description3Label = "What field of law is this listing for?",
                     Description4Label = "What 'buzz words' would you put in an ad?",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "What is your website?",
                     Contact1Label = "Phone (optional):",
                     Contact2Label = "Email (optional):",
                     Contact3Label = "",
                     Contact4Label = "",
                     BragLabel = "Brag a little",
                     PageTitle = "",
                     HeadingPartial = "_LawyersHeading"
                },
                new BusinessFormData
                {
                    FormName = "RestaurantCreate",
                     SearchDescription = EventDescription.Other,
                     BusinessType = BusinessType.Restaurant,
                     Title0Label = "What is the name of your eatery?",
                     Title1Label = "",
                     Description0Label = "What type of eatery is it?",
                     Description1Label = "What meals do you serve?",
                     Description2Label = "Popular menu items or your cuisine type?",
                     Description3Label = "Pricing?",
                     Description4Label = "",
                     Description5Label = "",
                     Description6Label = "",
                     Description7Label = "",
                     Description8Label = "",
                     Description9Label = "",
                     Contact0Label = "What is your website?",
                     Contact1Label = "Phone (optional):",
                     Contact2Label = "Email (optional):",
                     Contact3Label = "",
                     Contact4Label = "",
                     BragLabel = "Brag a little",
                     PageTitle = "",
                     HeadingPartial = "_RestaurantHeading"
                },
                new BusinessFormData
                {
                    FormName = "ServiceProvidersCreate",
                     SearchDescription = EventDescription.Other,
                     BusinessType = BusinessType.ServiceProviders,
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
                     HeadingPartial = "_ServiceProvidersHeading"
                },
                new BusinessFormData
                {
                    FormName = "ShopkeepersCreate",
                     SearchDescription = EventDescription.Other,
                     BusinessType = BusinessType.Shopkeepers,
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
                     HeadingPartial = "_ShopkeepersHeading"
                },
                new BusinessFormData
                {
                    FormName = "SupportCreate",
                     SearchDescription = EventDescription.Other,
                     BusinessType = BusinessType.Support,
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
                     HeadingPartial = "_SupportHeading"
                },
            };
        }
    }
}
