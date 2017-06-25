using MongoDB.Driver;
using System;
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

            var categories = db.GetCollection<Category>("categories");
            var newCategories = getSeedCategories();

            categories.InsertMany(newCategories);
        }

        static List<Category> getSeedCategories()
        {
            var result = new List<Category>()
            {
                new Category
                {
                    Name = "Fundraisers",
                    DisplayName = "Benefits & fundraisers",
                    Description ="when it's all about helping a good cause",
                    Type = CategoryType.Event,
                    DisplayOrder = 2,
                    Active = true
                },
                new Category
                {
                    Name = "Gigs",
                    DisplayName = "Gigs",
                    Description ="for local bands playing in local venues",
                    Type = CategoryType.Event,
                    DisplayOrder = 3,
                    Active = true
                },
                new Category
                {
                    Name = "ArtistsSolo",
                    DisplayName = "Individual Artists",
                    Description ="for individual performers appearing alone or together",
                    Type = CategoryType.Event,
                    DisplayOrder = 4,
                    Active = true
                },
                new Category
                {
                    Name = "ArtistsMultiple",
                    DisplayName = "Multiple Artists",
                    Description ="covers opening acts, joint appearances and/or special guests",
                    Type = CategoryType.Event,
                    DisplayOrder = 5,
                    Active = true
                },
                new Category
                {
                    Name = "Series",
                    DisplayName = "Series",
                    Description ="when the event is part of a series",
                    Type = CategoryType.Event,
                    DisplayOrder = 6,
                    Active = true
                },
                new Category
                {
                    Name = "Exhibits",
                    DisplayName = "Exhibits",
                    Description ="Is your work going to be on display somewhere?  choose this one!",
                    Type = CategoryType.Event,
                    DisplayOrder = 7,
                    Active = false
                },
                new Category
                {
                    Name = "Concerts",
                    DisplayName = "Concert Tours",
                    Description ="for any show on tour: concerts, comedians, speakers, whatever",
                    Type = CategoryType.Event,
                    DisplayOrder = 8,
                    Active = false
                },
                new Category
                {
                    Name="Orchestras",
                    DisplayName = "Troupes, Companies, Orchestras",
                    Description ="when who's putting on the show is as important as the show they're putting on",
                    Type = CategoryType.Event,
                    DisplayOrder = 9,
                    Active = false
                },
                new Category
                {
                    Name = "Others",
                    DisplayName = "Other",
                    Description ="any event that does not fall under any of the other categories, use this one!",
                    Type = CategoryType.Event,
                    DisplayOrder = 10,
                    Active = true
                }

            };
            return result;

        }
    }

}

