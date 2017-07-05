using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Looking2Console
{
    public class SearchTest
    {
        public SearchTest()
        {
            this.Client = new MongoClient("mongodb://localhost:27017");
            this.Db = Client.GetDatabase("searchtest");
            this.Listings = Db.GetCollection<Listing>("listings");
        }
        public MongoClient Client { get; set; }
        public IMongoDatabase Db { get; set; }
        public IMongoCollection<Listing> Listings { get; set; }

        public List<Listing> SearchDescription(string text, int maxResults = 0)
        {
            int limit = 100;
            if (maxResults > 0)
            {
                limit = maxResults;
            }
            var filter = new BsonDocument("Descriptions", 
                new BsonDocument("$regex", string.Format("(?i){0}",text)));

            return Listings.Find<Listing>(filter)
                            .Limit(limit)
                            .ToList();
                
        }

        public long SearchDescriptionCount(string text)
        {
            var filter = new BsonDocument("Descriptions",
                new BsonDocument("$regex", string.Format("(?i){0}", text)));
            return Listings.Count(filter);
        }

        public long SearchLocationDescriptionCount(string location, string description)
        {
            var filter = new BsonDocument("$and", new BsonArray
            {
                new BsonDocument("Descriptions", new BsonDocument("$regex", string.Format("(?i){0}", description))),
                new BsonDocument("Location", new BsonDocument("$regex", string.Format("(?i){0}", location)))
            });
            return Listings.Count(filter);
        }

        //public long SearchArrayCount(string arrayField, )

        public void Seed(int number)
        {
            Db.DropCollection("listings");

            var artists = getArtists();
            var venues = getVenues();
            var ages = getAges();
            var genres = getGenres();
            var cities = getCities();
            var areas = getAreas();
            var states = getStates();
            var times = getTimes();
            var frequ = getFrequencies();

            Random venueRand = new Random();
            Random agesRand = new Random();
            Random genresRand = new Random();
            Random citiesRand = new Random();
            Random areasRand = new Random();
            Random statesRand = new Random();
            Random timesRand = new Random();
            Random freqRand = new Random();
            int j = 0;
            int countPerArtist = number / artists.Count;
            foreach (var artist in artists)
            {
                for (int i = 0; i <= countPerArtist; i++)
                {
                    int venueIndex = venueRand.Next(venues.Count);
                    int agesIndex = agesRand.Next(ages.Count);
                    int genresIndex = genresRand.Next(genres.Count);
                    int citiesIndex = citiesRand.Next(cities.Count);
                    int areasIndex = areasRand.Next(areas.Count);
                    int statesIndex = statesRand.Next(states.Count);
                    int timesIndex = timesRand.Next(times.Count);
                    int freqIndex = freqRand.Next(frequ.Count);

                    var listing = new Listing();
                    listing.Titles.Add(artist.Value);
                    listing.Titles.Add(venues[venueIndex]);
                    listing.Descriptions.Add(genres[genresIndex]);
                    listing.Descriptions.Add(ages[agesIndex]);
                    listing.Descriptions.Add(frequ[freqIndex]);
                    listing.Descriptions.Add(times[timesIndex]);
                    listing.Location.Add(states[statesIndex]);
                    listing.Location.Add(cities[citiesIndex]);
                    listing.Location.Add(areas[areasIndex]);
                    Listings.InsertOne(listing);
                    j++;
                }
                
            }
            Console.WriteLine(j.ToString());
            

        }

        private Dictionary<int,string> getArtists()
        {
            return new Dictionary<int, string>()
            {
                {0, "Blink-182" },
                {1, "Class of '99" },
                {2, "Cold War Kids" },
                {3, "Chiodos" },
                {4, "Foo Fighters" },
                {5, "Fireflight" },
                {6, "Hellogoodbye" },
                {7, "I See Stars" },
                {8, "James 'The Rev' Sullivan" },
                {9, "Nine Inch Nails" },
                {10, "Nine Lashes" },
                {11, "No Doubt" },
                {12, "Taking Back Sunday" },
                {13, "The Used" },
                {14, "You Me At Six" },
                {15, "Postal Service" },
                {16, "NOFX" },
                {17, "MGMT" },
                {18, "Modest Mouse" },
                {19, "Ed Sheeran" }
            };
        }

        private Dictionary<int,string> getVenues()
        {
            return new Dictionary<int, string>()
            {
                { 0,"Pepsi Center" },
                { 1, "PNC Park" },
                { 2, "Heinz Field"},
                { 3, "Red Rocks" },
                { 4, "The Park House" },
                { 5, "Vue 412" },
                { 6, "Carnegie of Homestead Music Hall" },
                { 7, "Jim's Tavern" },
                { 8, "Herman's Hideaway" },
                { 9, "Swallow Hill Music" },
                { 10, "Hi-Dive" },
                { 11, "Fillmore Auditorium" },
                { 12, "Ogden Theater" },
                { 13, "Heinz Hall" },
                { 14, "Smiling Moose" },
                { 15, "Rex Theater" },
                { 16, "Brillobox" },
                { 17, "Stage AE" },
                { 18, "PPG Paints Arena" },
                { 19, "Diesel Club Lounge" },
            };
        }

        private Dictionary<int,string> getAges()
        {
            return new Dictionary<int, string>()
            {
                
                {0, "20+" },
                {1, "all ages" },
                {2, "over 21" },
                {3, "21 and over" },
                {4, "adults only" },
                {5, "mature" },
                {6, "4-10" },
                {7, "children" },
                {8, "toddlers" },
            };
        }

        private Dictionary<int,string> getGenres()
        {
            return new Dictionary<int, string>()
            {
                {0, "Reggae" },
                {1, "bluegrass" },
                {2, "bluegrass & new grass" },
                {3, "rock & roll" },
                {4, "rock" },
                {5, "hard rock" },
                {6, "Christian" },
                {7, "Fundraiser" },
                {8, "Benefit" },
                {9, "gospel" },
                {10, "Country" },
                {11, "Classical" },
                {12, "Big band" },
                {13, "80s" },
                {14, "metal" },
            };
        }

        private Dictionary<int,string> getCities()
        {
            return new Dictionary<int, string>()
            {
                {0, "Pittsburgh" },
                {1, "Denver" },
                {2, "Cleveland" },
                {3, "Morrison" },
                {4, "Littleton" },
                {5, "Monroeville" },
                {6, "Nashville" },
                {7, "Colorado Springs" }

            };
        }

        private Dictionary<int, string> getAreas()
        {
            return new Dictionary<int, string>()
            {
                {0, "Shadyside" },
                {1, "North shore" },
                {2, "Highlands" },
                {3, "North Hills" },
                {4, "South Hills" },
                {5, "Allegheny" },
                {6, "Chatfield" },
                {7, "Highlands Ranch" },
                {8, "LODO" },
                {9, "Carnegie" },
                {10, "Dormont"},
                {11, "Oakland" },
                {12, "Penn Hills" },
                {13, "Hill District" },
                {14, "Swickley" },
                {15, "Harmarville" },
                {16, "Fox Chapel" },
                {17, "Moon" },
            };
        }

        private Dictionary<int, string> getStates()
        {
            return new Dictionary<int, string>()
            {
                {0, "PA" },
                {1, "CO" },
                {2, "OH" },
                {3, "TN" }
            };
        }

        private Dictionary<int, string> getTimes()
        {
            return new Dictionary<int, string>()
            {
                {0, "8pm" },
                {1, "7pm" },
                {2, "doors open at 8" },
                {3, "doors open at 7pm" },
                {4, "7-10" },
                {5, "7am-10am" },
                {6, "7:30pm to 10:00pm" },
                {7, "10:00 to midnight" },
                {8, "10pm to 11pm" },
                {9, "10:00pm" },
            };
        }

        private Dictionary<int, string> getFrequencies()
        {
            return new Dictionary<int, string>()
            {
                {0, "Every Friday" },
                {1, "every other wednesday" },
                {2, "first tuesday of the month" },
                {3, "one night only" },
                {4, "every afternoon" }
            };
        }
    }

    public class Listing
    {
        public Listing()
        {
            this.Titles = new List<string>();
            this.Descriptions = new List<string>();
            this.Contact = new List<string>();
            this.Location = new List<string>();
        }
        public ObjectId Id { get; set; }
        public List<string> Titles { get; set; }
        public List<string> Descriptions { get; set; }
        public List<string> Contact { get; set; }
        public List<string> Location { get; set; }
    }
}
