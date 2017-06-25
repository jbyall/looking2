using Looking2.Web.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.DataAccess
{
    public class Repository
    {
        private IMongoCollection<Gig> gigsCollection;
        public Repository()
        {
            this.Client = new MongoClient("mongodb://localhost:27017");
            this.Database = Client.GetDatabase("looking2");
            this.gigsCollection = Database.GetCollection<Gig>("gigs");
        }
        public MongoClient Client{ get; set; }
        public IMongoDatabase Database { get; set; }

        public void InsertCat(Cat newCat)
        {
            var collection = Database.GetCollection<Cat>("cats");
            collection.InsertOne(newCat);

        }

        public List<Category> GetCategoriesByType(CategoryType type, bool activeOnly = false)
        {
            var collection = Database.GetCollection<Category>("categories");
            var result = collection.AsQueryable()
                .Where(c => c.Type == CategoryType.Event)
                .OrderBy(c => c.DisplayOrder)
                .ToList();
            return result;
        }

        public Gig InsertGig(Gig gig)
        {
            var collection = Database.GetCollection<Gig>("gigs");
            collection.InsertOne(gig);
            return gig;
        }

        public Gig FindGig(string id)
        {
            var collection = Database.GetCollection<Gig>("gigs");
            var result = collection.AsQueryable().Where(g => g.Id == new ObjectId(id)).SingleOrDefault();
            return result;
        }

        public List<Gig> Gigs()
        {
            return gigsCollection.AsQueryable().ToList();
        }
    }
}
