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
            var test = new SearchTest();
            test.Seed(50);
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

        
    }

}

