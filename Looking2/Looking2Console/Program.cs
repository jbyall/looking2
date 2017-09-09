using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Linq;
using System.Collections.Generic;
using Looking2.Web.Domain;


namespace Looking2Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("looking2");
            addCategoryToBusinesses(client, db);


            //var test = new SearchTest();
            //test.Seed(50);
            //var bluegrassResults = test.SearchDescription("bluegrass");
            //var BluegrassResults = test.SearchDescription("Bluegrass", 10);

            //var bluegrassCount = test.SearchDescriptionCount("rock");
            //var descLocCount = test.SearchLocationDescriptionCount("denver", "rock");
            Console.WriteLine("Done");
            Console.Read();
        }

        static void addEventCategories()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("looking2");

            var listCollection = db.GetCollection<EventListing>("listings");
            

            var categories = db.GetCollection<Category>("categories");
            var newCategories = getSeedCategories();

            foreach (var item in newCategories)
            {
                // Check for existing to determine insert or update
                var existingCategory = categories.Find(c => c.Name == item.Name).ToList();

                // Insert new
                if (existingCategory.Count < 1)
                {
                    categories.InsertOne(item);
                }
                // Update
                else
                {
                    item.Id = existingCategory[0].Id;
                    var result = categories.FindOneAndReplace<Category>(c => c.Name == item.Name, item);
                }

            }
            
        }

        static List<Category> getSeedCategories()
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
                    Active = false
                },
                new Category
                {
                    Name = "Concert",
                    DisplayName = "Concert Tours",
                    Description ="for any show on tour: concerts, comedians, speakers, whatever",
                    Type = ListingCategory.Event,
                    Active = false
                },
                new Category
                {
                    
                    Name = "Orchestra",
                    DisplayName = "Troupes, Companies, Orchestras",
                    Description ="when who's putting on the show is as important as the show they're putting on",
                    Type = ListingCategory.Event,
                    Active = false
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
                    Active = true
                },
                new Category
                {
                    Name="Shopkeepers",
                    DisplayName = "Shopkeepers",
                    Description ="If you sell (or rent out) stuff for a livelihood, choose this one, even if it's from out of your kitchen",
                    Type = ListingCategory.Business,
                    Active = true
                },
                new Category
                {
                    Name="Support",
                    DisplayName = "Support",
                    Description ="Do you offer support of any kind?",
                    Type = ListingCategory.Business,
                    Active = true
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

        static void addCategoryToBusinesses(MongoClient client, IMongoDatabase db)
        {
            var businessCollection = db.GetCollection<BusinessListing>("businesses");
            var businesses = businessCollection.AsQueryable().ToList();
                             
            foreach (var item in businesses)
            {
                switch (item.BusinessType)
                {
                    case BusinessType.Artists:
                        item.Category = BusinessSearchCategory.Art;
                        break;
                    case BusinessType.HealthCare:
                        item.Category = BusinessSearchCategory.Healthcare;
                        break;
                    case BusinessType.AltHealthCare:
                        item.Category = BusinessSearchCategory.HealthcareAlt;
                        break;
                    case BusinessType.Information:
                        item.Category = BusinessSearchCategory.SupportInformation;
                        break;
                    case BusinessType.Instruction:
                        item.Category = BusinessSearchCategory.Lessons;
                        break;
                    case BusinessType.Lawyers:
                        item.Category = BusinessSearchCategory.Lawyers;
                        break;
                    case BusinessType.Restaurant:
                        item.Category = BusinessSearchCategory.FoodAndBeverage;
                        break;
                    case BusinessType.ServiceProviders:
                        // TODO : Similar to BusinessType.Other
                        break;
                    case BusinessType.Shopkeepers:
                        item.Category = BusinessSearchCategory.Retail;
                        break;
                    case BusinessType.Support:
                        item.Category = BusinessSearchCategory.SupportInformation;
                        break;
                    case BusinessType.Other:
                        #region Other
                        var categoryDet = item.Descriptions[0].ToLower();
                        if (categoryDet.Contains("sports"))
                        {
                            item.Category = BusinessSearchCategory.SportFitness;
                        }

                        if (categoryDet.Contains("window blinds") || categoryDet.Contains("storage") || categoryDet.Contains("moving supplies") || categoryDet.Contains("duct system"))
                        {
                            item.Category = BusinessSearchCategory.HomeServices;
                        }

                        if (categoryDet.Contains("lesson"))
                        {
                            item.Category = BusinessSearchCategory.Lessons;
                        }

                        if (categoryDet.Contains("to eat") || categoryDet.Contains("bakery") || categoryDet.Contains("catering") || categoryDet.Contains("snack") ||
                            categoryDet.Contains("beverage") || categoryDet.Contains("candy") || categoryDet.Contains("butcher"))
                        {
                            item.Category = BusinessSearchCategory.FoodAndBeverage;
                        }

                        if (categoryDet.Contains("message") || categoryDet.Contains("audiologist") || categoryDet.Contains("dermatolog") || categoryDet.Contains("health care") ||
                            categoryDet.Contains("pedorth") || categoryDet.Contains("sleep coach") || categoryDet.Contains("horolog"))
                        {
                            item.Category = BusinessSearchCategory.Healthcare;
                        }

                        if (categoryDet.Contains("running") || categoryDet.Contains("buy & sell") || categoryDet.Contains("movies") || categoryDet.Contains("games") ||
                            categoryDet.Contains("antique"))
                        {
                            item.Category = BusinessSearchCategory.Retail;
                        }

                        if (categoryDet.Contains("equipment") || categoryDet.Contains("swimming pools") || categoryDet.Contains("sandblasting"))
                        {
                            item.Category = BusinessSearchCategory.Supplies;
                        }

                        if (categoryDet.Contains("tires") || categoryDet.Contains("mechanic") || categoryDet.Contains("car care") || categoryDet.Contains("vehicles") ||
                            categoryDet.Contains("alignments") || categoryDet.Contains("car dealership") || categoryDet.Contains("motorcycle") || categoryDet.Contains("auto parts") ||
                            categoryDet.Contains("truck parts") || categoryDet.Contains("dump trailers"))
                        {
                            item.Category = BusinessSearchCategory.AutoVehicle;
                        }

                        if (categoryDet.Contains("attorney") || categoryDet.Contains("lawyer"))
                        {
                            item.Category = BusinessSearchCategory.Lawyers;
                        }

                        if (categoryDet.Contains("hacking") || categoryDet.Contains("financial") || categoryDet.Contains("cleaning") || categoryDet.Contains("consulting") ||
                            categoryDet.Contains("fire escape") || categoryDet.Contains("banking") || categoryDet.Contains("memorials") || categoryDet.Contains("recycling"))
                        {
                            item.Category = BusinessSearchCategory.Services;
                        }

                        if (categoryDet.Contains("movie house") || categoryDet.Contains("drive-in") || categoryDet.Contains("gravesite") || categoryDet.Contains("museum") ||
                            categoryDet.Contains("black history") || categoryDet.Contains("arcade") || categoryDet.Contains("amusement") || categoryDet.Contains("convention"))
                        {
                            item.Category = BusinessSearchCategory.EntertainmentAttractions;
                        }

                        if (categoryDet.Contains("salon") || categoryDet.Contains("tattoo") || categoryDet.Contains("hair") || categoryDet.Contains("beauty"))
                        {
                            item.Category = BusinessSearchCategory.Salons;
                        }

                        if (categoryDet.Contains("veterina") || categoryDet.Contains("pets") || categoryDet.Contains("fish"))
                        {
                            item.Category = BusinessSearchCategory.PetAnimal;
                        }

                        if (categoryDet.Contains("adoption") || categoryDet.Contains("civil rights") || categoryDet.Contains("law enforcement"))
                        {
                            item.Category = BusinessSearchCategory.HumanServices;
                        }

                        if (categoryDet.Contains("public transport"))
                        {
                            item.Category = BusinessSearchCategory.PublicServices;
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
                

                businessCollection.FindOneAndReplace<BusinessListing>(l => l.Id == item.Id, item);

            }
        }
    }

}

