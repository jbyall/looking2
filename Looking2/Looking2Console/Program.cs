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
            addEventCategories();
            Console.WriteLine("Hello World!");
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
                var existingCategory = categories.Find(c => c.Name == item.Name).ToList();
                if (existingCategory.Count < 1)
                {
                    categories.InsertOne(item);
                }
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
                    Name = "Fundraiser",
                    DisplayName = "Benefits & fundraisers",
                    Description ="when it's all about helping a good cause",
                    Type = CategoryType.Event,
                    DisplayOrder = 2,
                    Active = true
                },
                new Category
                {
                    Name = "Gig",
                    DisplayName = "Gigs",
                    Description ="for local bands playing in local venues",
                    Type = CategoryType.Event,
                    DisplayOrder = 3,
                    Active = true
                },
                new Category
                {
                    Name="ArtistIndividual",
                    DisplayName = "Individual Artists",
                    Description ="for individual performers appearing alone or together",
                    Type = CategoryType.Event,
                    DisplayOrder = 4,
                    Active = true
                },
                new Category
                {
                    Name = "ArtistMultiple",
                    DisplayName = "Multiple Artists",
                    Description ="covers opening acts, joint appearances and/or special guests",
                    Type = CategoryType.Event,
                    DisplayOrder = 5,
                    Active = true
                },
                new Category
                {
                    Name="Series",
                    DisplayName = "Series",
                    Description ="when the event is part of a series",
                    Type = CategoryType.Event,
                    DisplayOrder = 6,
                    Active = true
                },
                new Category
                {
                    Name = "Exhibit",
                    DisplayName = "Exhibits",
                    Description ="Is your work going to be on display somewhere?  choose this one!",
                    Type = CategoryType.Event,
                    DisplayOrder = 7,
                    Active = false
                },
                new Category
                {
                    Name = "Concert",
                    DisplayName = "Concert Tours",
                    Description ="for any show on tour: concerts, comedians, speakers, whatever",
                    Type = CategoryType.Event,
                    DisplayOrder = 8,
                    Active = false
                },
                new Category
                {
                    
                    Name = "Orchestra",
                    DisplayName = "Troupes, Companies, Orchestras",
                    Description ="when who's putting on the show is as important as the show they're putting on",
                    Type = CategoryType.Event,
                    DisplayOrder = 9,
                    Active = false
                },
                new Category
                {
                    Name="Other",
                    DisplayName = "Other",
                    Description ="any event that does not fall under any of the other categories, use this one!",
                    Type = CategoryType.Event,
                    DisplayOrder = 10,
                    Active = true
                },
                new Category
                {
                    Name = "Restaurant",
                    DisplayName = "Restaurants",
                    Description ="any event that does not fall under any of the other categories, use this one!",
                    Type = CategoryType.Business,
                    DisplayOrder = 10,
                    Active = true
                }


            };
            return result;

        }

        
    }

}

