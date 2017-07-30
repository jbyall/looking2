using Looking2.Web.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Looking2.Web.Settings;
using System.Linq.Expressions;

namespace Looking2.Web.DataAccess
{
    public class SeedRepository
    {
        public SeedRepository()
        {
            this.Client = new MongoClient("mongodb://localhost:27017/looking2-seed");
            this.Db = Client.GetDatabase("looking2-seed");
            this.Categories = Db.GetCollection<Category>("categories");
        }

        public MongoClient Client { get; set; }
        public IMongoDatabase Db { get; set; }
        public IMongoCollection<Category> Categories { get; set; }

        public List<Category> GetSeedCategories()
        {
            return this.Categories.AsQueryable().ToList();
        }
    }
}
