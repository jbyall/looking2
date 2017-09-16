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
            // TODO : Pull latest from prod and run this script to update categories as needed.
            // Then, push the updates to prod
            // ALSO - Update Listing create to add appropriate category

            //MongoClient client = new MongoClient("mongodb://localhost:27017");
            MongoClient client = new MongoClient("mongodb://ec2-34-228-29-104.compute-1.amazonaws.com:27017");
            var db = client.GetDatabase("looking2");

            doMigration(client, db);
            Console.Read();

            //var connectionString = "mongodb://localhost:27017";
            //BusinessReport report = new BusinessReport(connectionString);
            //var results = new List<BusinessCategoryResult>();
            //var bizTypes = Enum.GetValues(typeof(BusinessType));
            //foreach (var item in bizTypes)
            //{
            //    results.Add(report.GetCategoriesInBusinessType((BusinessType)item));
            //}

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.ListingType}");
            //    foreach (var subItem in item.TypeCount)
            //    {
            //        Console.WriteLine($"--{subItem.Key} = {subItem.Value}");
            //    }
            //}
            //Console.Read();
        }

        static void doMigration(MongoClient client, IMongoDatabase db)
        {
            Migrator migrator = new Migrator(client, db);
            var stuff = migrator.GetMigrationSummary();
            migrator.Migrate();
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

        //static void addCategoryToBusinesses(MongoClient client, IMongoDatabase db)
        //{
        //    var businessCollection = db.GetCollection<BusinessListing>("businesses");
        //    var businesses = businessCollection.AsQueryable().ToList();
                             
        //    foreach (var item in businesses)
        //    {
        //        switch (item.BusinessType)
        //        {
        //            case BusinessType.Artists:
        //                item.Categories = BusinessSearchCategory.Art;
        //                break;
        //            case BusinessType.HealthCare:
        //                item.Categories = BusinessSearchCategory.Healthcare;
        //                break;
        //            case BusinessType.AltHealthCare:
        //                item.Categories = BusinessSearchCategory.HealthcareAlt;
        //                break;
        //            case BusinessType.Information:
        //                item.Categories = BusinessSearchCategory.SupportInformation;
        //                break;
        //            case BusinessType.Instruction:
        //                item.Categories = BusinessSearchCategory.Lessons;
        //                break;
        //            case BusinessType.Lawyers:
        //                item.Categories = BusinessSearchCategory.Lawyers;
        //                break;
        //            case BusinessType.Restaurant:
        //                item.Categories = BusinessSearchCategory.FoodAndBeverage;
        //                break;
        //            case BusinessType.ServiceProviders:
        //                // TODO : Similar to BusinessType.Other
        //                #region Other
        //                var categoryDet = item.Descriptions[0].ToLower();
        //                if (categoryDet.Contains("sports"))
        //                {
        //                    item.Categories = BusinessSearchCategory.SportFitness;
        //                }

        //                if (categoryDet.Contains("window blinds") || categoryDet.Contains("storage") || categoryDet.Contains("moving supplies") || categoryDet.Contains("duct system"))
        //                {
        //                    item.Categories = BusinessSearchCategory.HomeServices;
        //                }

        //                if (categoryDet.Contains("lesson"))
        //                {
        //                    item.Categories = BusinessSearchCategory.Lessons;
        //                }

        //                if (categoryDet.Contains("to eat") || categoryDet.Contains("bakery") || categoryDet.Contains("catering") || categoryDet.Contains("snack") ||
        //                    categoryDet.Contains("beverage") || categoryDet.Contains("candy") || categoryDet.Contains("butcher"))
        //                {
        //                    item.Categories = BusinessSearchCategory.FoodAndBeverage;
        //                }

        //                if (categoryDet.Contains("message") || categoryDet.Contains("audiologist") || categoryDet.Contains("dermatolog") || categoryDet.Contains("health care") ||
        //                    categoryDet.Contains("pedorth") || categoryDet.Contains("sleep coach") || categoryDet.Contains("horolog"))
        //                {
        //                    item.Categories = BusinessSearchCategory.Healthcare;
        //                }

        //                if (categoryDet.Contains("running") || categoryDet.Contains("buy & sell") || categoryDet.Contains("movies") || categoryDet.Contains("games") ||
        //                    categoryDet.Contains("antique"))
        //                {
        //                    item.Categories = BusinessSearchCategory.Retail;
        //                }

        //                if (categoryDet.Contains("equipment") || categoryDet.Contains("swimming pools") || categoryDet.Contains("sandblasting"))
        //                {
        //                    item.Categories = BusinessSearchCategory.Supplies;
        //                }

        //                if (categoryDet.Contains("tires") || categoryDet.Contains("mechanic") || categoryDet.Contains("car care") || categoryDet.Contains("vehicles") ||
        //                    categoryDet.Contains("alignments") || categoryDet.Contains("car dealership") || categoryDet.Contains("motorcycle") || categoryDet.Contains("auto parts") ||
        //                    categoryDet.Contains("truck parts") || categoryDet.Contains("dump trailers"))
        //                {
        //                    item.Categories = BusinessSearchCategory.AutoVehicle;
        //                }

        //                if (categoryDet.Contains("attorney") || categoryDet.Contains("lawyer"))
        //                {
        //                    item.Categories = BusinessSearchCategory.Lawyers;
        //                }

        //                if (categoryDet.Contains("hacking") || categoryDet.Contains("financial") || categoryDet.Contains("cleaning") || categoryDet.Contains("consulting") ||
        //                    categoryDet.Contains("fire escape") || categoryDet.Contains("banking") || categoryDet.Contains("memorials") || categoryDet.Contains("recycling"))
        //                {
        //                    item.Categories = BusinessSearchCategory.GeneralServices;
        //                }

        //                if (categoryDet.Contains("movie house") || categoryDet.Contains("drive-in") || categoryDet.Contains("gravesite") || categoryDet.Contains("museum") ||
        //                    categoryDet.Contains("black history") || categoryDet.Contains("arcade") || categoryDet.Contains("amusement") || categoryDet.Contains("convention"))
        //                {
        //                    item.Categories = BusinessSearchCategory.EntertainmentAttractions;
        //                }

        //                if (categoryDet.Contains("salon") || categoryDet.Contains("tattoo") || categoryDet.Contains("hair") || categoryDet.Contains("beauty"))
        //                {
        //                    item.Categories = BusinessSearchCategory.Salons;
        //                }

        //                if (categoryDet.Contains("veterina") || categoryDet.Contains("pets") || categoryDet.Contains("fish"))
        //                {
        //                    item.Categories = BusinessSearchCategory.PetAnimal;
        //                }

        //                if (categoryDet.Contains("adoption") || categoryDet.Contains("civil rights") || categoryDet.Contains("law enforcement"))
        //                {
        //                    item.Categories = BusinessSearchCategory.HumanServices;
        //                }

        //                if (categoryDet.Contains("public transport"))
        //                {
        //                    item.Categories = BusinessSearchCategory.PublicServices;
        //                }
        //                #endregion
        //                break;
        //            case BusinessType.Shopkeepers:
        //                item.Categories = BusinessSearchCategory.Retail;
        //                break;
        //            case BusinessType.Support:
        //                item.Categories = BusinessSearchCategory.SupportInformation;
        //                break;
        //            case BusinessType.Other:
        //                #region Other
        //                var categoryDet2 = item.Descriptions[0].ToLower();
        //                if (categoryDet2.Contains("sports"))
        //                {
        //                    item.Categories = BusinessSearchCategory.SportFitness;
        //                }

        //                if (categoryDet2.Contains("window blinds") || categoryDet2.Contains("storage") || categoryDet2.Contains("moving supplies") || categoryDet2.Contains("duct system"))
        //                {
        //                    item.Categories = BusinessSearchCategory.HomeServices;
        //                }

        //                if (categoryDet2.Contains("lesson"))
        //                {
        //                    item.Categories = BusinessSearchCategory.Lessons;
        //                }

        //                if (categoryDet2.Contains("to eat") || categoryDet2.Contains("bakery") || categoryDet2.Contains("catering") || categoryDet2.Contains("snack") ||
        //                    categoryDet2.Contains("beverage") || categoryDet2.Contains("candy") || categoryDet2.Contains("butcher"))
        //                {
        //                    item.Categories = BusinessSearchCategory.FoodAndBeverage;
        //                }

        //                if (categoryDet2.Contains("message") || categoryDet2.Contains("audiologist") || categoryDet2.Contains("dermatolog") || categoryDet2.Contains("health care") ||
        //                    categoryDet2.Contains("pedorth") || categoryDet2.Contains("sleep coach") || categoryDet2.Contains("horolog"))
        //                {
        //                    item.Categories = BusinessSearchCategory.Healthcare;
        //                }

        //                if (categoryDet2.Contains("running") || categoryDet2.Contains("buy & sell") || categoryDet2.Contains("movies") || categoryDet2.Contains("games") ||
        //                    categoryDet2.Contains("antique"))
        //                {
        //                    item.Categories = BusinessSearchCategory.Retail;
        //                }

        //                if (categoryDet2.Contains("equipment") || categoryDet2.Contains("swimming pools") || categoryDet2.Contains("sandblasting"))
        //                {
        //                    item.Categories = BusinessSearchCategory.Supplies;
        //                }

        //                if (categoryDet2.Contains("tires") || categoryDet2.Contains("mechanic") || categoryDet2.Contains("car care") || categoryDet2.Contains("vehicles") ||
        //                    categoryDet2.Contains("alignments") || categoryDet2.Contains("car dealership") || categoryDet2.Contains("motorcycle") || categoryDet2.Contains("auto parts") ||
        //                    categoryDet2.Contains("truck parts") || categoryDet2.Contains("dump trailers"))
        //                {
        //                    item.Categories = BusinessSearchCategory.AutoVehicle;
        //                }

        //                if (categoryDet2.Contains("attorney") || categoryDet2.Contains("lawyer"))
        //                {
        //                    item.Categories = BusinessSearchCategory.Lawyers;
        //                }

        //                if (categoryDet2.Contains("hacking") || categoryDet2.Contains("financial") || categoryDet2.Contains("cleaning") || categoryDet2.Contains("consulting") ||
        //                    categoryDet2.Contains("fire escape") || categoryDet2.Contains("banking") || categoryDet2.Contains("memorials") || categoryDet2.Contains("recycling"))
        //                {
        //                    item.Categories = BusinessSearchCategory.GeneralServices;
        //                }

        //                if (categoryDet2.Contains("movie house") || categoryDet2.Contains("drive-in") || categoryDet2.Contains("gravesite") || categoryDet2.Contains("museum") ||
        //                    categoryDet2.Contains("black history") || categoryDet2.Contains("arcade") || categoryDet2.Contains("amusement") || categoryDet2.Contains("convention"))
        //                {
        //                    item.Categories = BusinessSearchCategory.EntertainmentAttractions;
        //                }

        //                if (categoryDet2.Contains("salon") || categoryDet2.Contains("tattoo") || categoryDet2.Contains("hair") || categoryDet2.Contains("beauty"))
        //                {
        //                    item.Categories = BusinessSearchCategory.Salons;
        //                }

        //                if (categoryDet2.Contains("veterina") || categoryDet2.Contains("pets") || categoryDet2.Contains("fish"))
        //                {
        //                    item.Categories = BusinessSearchCategory.PetAnimal;
        //                }

        //                if (categoryDet2.Contains("adoption") || categoryDet2.Contains("civil rights") || categoryDet2.Contains("law enforcement"))
        //                {
        //                    item.Categories = BusinessSearchCategory.HumanServices;
        //                }

        //                if (categoryDet2.Contains("public transport"))
        //                {
        //                    item.Categories = BusinessSearchCategory.PublicServices;
        //                }
        //                #endregion
        //                break;
        //            default:
        //                break;
        //        }
                

        //        businessCollection.FindOneAndReplace<BusinessListing>(l => l.Id == item.Id, item);

        //    }
        //}
    }

}

