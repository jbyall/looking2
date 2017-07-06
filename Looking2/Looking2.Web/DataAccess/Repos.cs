using Looking2.Web.Domain;
using Looking2.Web.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.DataAccess
{
    public enum SearchOperator
    {
        And,
        Or
    }
    public interface ICategoriesRepository : IRepository<Category>
    {
        IEnumerable<Category> GetByType(ListingCategory type);
    }
    public class CategoriesRepository : Repository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(IOptions<DbSettings> settings) : base(settings)
        {
            this.Collection = Db.GetCollection<Category>(settings.Value.CategoriesCollection);
        }

        public IEnumerable<Category> GetByType(ListingCategory type)
        {
            return this.Collection.AsQueryable().Where(c => c.Type == type);
        }
    }

    public interface IEventsRepository : IRepository<EventListing>
    {
        List<EventListing> SearchDescriptionFields(string text, int maxResults = 100);
        List<EventListing> SearchTitleAndDescription(string title, string description, SearchOperator searchType, int maxResults = 100);
    }
    public class EventsRepository : Repository<EventListing>, IEventsRepository
    {
        public EventsRepository(IOptions<DbSettings> settings) : base(settings)
        {
            this.Collection = Db.GetCollection<EventListing>(settings.Value.EventsCollection);
        }

        public List<EventListing> SearchDescriptionFields(string text, int maxResults = 100)
        {
            var filter = new BsonDocument("Descriptions", new BsonDocument("$regex", string.Format("(?i){0}", text)));
            return this.Collection.Find<EventListing>(filter)
                            .Limit(maxResults)
                            .ToList();
        }

        public List<EventListing> SearchTitleAndDescription(string title, string description, SearchOperator searchType,int maxResults = 100)
        {
            string searchOp = "";
            switch (searchType)
            {
                case SearchOperator.And:
                    searchOp = "$and";
                    break;
                case SearchOperator.Or:
                    searchOp = "$or";
                    break;
                default:
                    break;
            }
            var filter = new BsonDocument(searchOp, new BsonArray
            {
                new BsonDocument("Titles", new BsonDocument("$regex", string.Format("(?i){0}", title))),
                new BsonDocument("Descriptions", new BsonDocument("$regex", string.Format("(?i){0}", description))),
            });
            return this.Collection.Find<EventListing>(filter)
                            .Limit(maxResults)
                            .ToList();
        }
    }

    public interface IBusinessRepository : IRepository<BusinessListing>
    {
        List<BusinessListing> SearchDescriptionFields(string text, int maxResults = 100);
        List<BusinessListing> SearchTitleAndDescription(string title, string description, SearchOperator searchType, int maxResults = 100);
    }
    public class BusinessRepository : Repository<BusinessListing>, IBusinessRepository
    {
        public BusinessRepository(IOptions<DbSettings> settings) : base(settings)
        {
            this.Collection = Db.GetCollection<BusinessListing>(settings.Value.BusinessesCollection);
        }

        public List<BusinessListing> SearchDescriptionFields(string text, int maxResults = 100)
        {
            var filter = new BsonDocument("Descriptions", new BsonDocument("$regex", string.Format("(?i){0}", text)));
            return this.Collection.Find<BusinessListing>(filter)
                            .Limit(maxResults)
                            .ToList();
        }

        public List<BusinessListing> SearchTitleAndDescription(string title, string description, SearchOperator searchType, int maxResults = 100)
        {
            string searchOp = "";
            switch (searchType)
            {
                case SearchOperator.And:
                    searchOp = "$and";
                    break;
                case SearchOperator.Or:
                    searchOp = "$or";
                    break;
                default:
                    break;
            }
            var filter = new BsonDocument(searchOp, new BsonArray
            {
                new BsonDocument("Titles", new BsonDocument("$regex", string.Format("(?i){0}", title))),
                new BsonDocument("Descriptions", new BsonDocument("$regex", string.Format("(?i){0}", description))),
            });
            return this.Collection.Find<BusinessListing>(filter)
                            .Limit(maxResults)
                            .ToList();
        }
        
        // TODO:
        //1. Add an exists method
        //2. Try to combine BusinessRepository and EventRepository into 1 generic
    }

    public interface IEventFormsRepo : IRepository<EventFormData>
    {
        EventFormData GetByName(string name);
    }
    public class EventFormsRepository : Repository<EventFormData>, IEventFormsRepo
    {
        public EventFormsRepository(IOptions<DbSettings> settings) : base(settings)
        {
            this.Collection = Db.GetCollection<EventFormData>(settings.Value.EventFormsCollection);
        }

        public EventFormData GetByName(string name)
        {
            return this.Collection.AsQueryable().Where(f => f.FormName.ToLower() == name.ToLower()).SingleOrDefault();
        }
    }

    public interface IBusinessFormsRepo : IRepository<BusinessFormData>
    {
        BusinessFormData GetByName(string name);
    }
    public class BusinessFormsRepository : Repository<BusinessFormData>, IBusinessFormsRepo
    {
        public BusinessFormsRepository(IOptions<DbSettings> settings) : base(settings)
        {
            this.Collection = Db.GetCollection<BusinessFormData>(settings.Value.BusinessFormsCollection);
        }

        public BusinessFormData GetByName(string name)
        {
            return this.Collection.AsQueryable().Where(f => f.FormName.ToLower()==name.ToLower()).SingleOrDefault();
        }
    }


    #region OldRepos
    //public interface IGigsRepository
    //{
    //    Gig FindGig(string id);
    //    IEnumerable<Gig> FindGigs();
    //    Gig InsertGig(Gig gig);


    //}
    //public class GigsRepository : Repository<Gig>
    //{
    //    public GigsRepository() : base("gigs")
    //    {
    //    }
    //}

    //public class GigsRepository : IGigsRepository
    //{
    //    private MongoClient client;
    //    private IMongoDatabase database;
    //    private IMongoCollection<Gig> gigsCollection;

    //    public GigsRepository()
    //    {
    //        this.client = new MongoClient("mongodb://localhost:27017");
    //        this.database = client.GetDatabase("looking2");
    //        this.gigsCollection = database.GetCollection<Gig>("gigs");
    //    }

    //    public Gig FindGig(string id)
    //    {
    //        var result = gigsCollection.AsQueryable().Where(g => g.Id == new ObjectId(id)).SingleOrDefault();
    //        return result;
    //    }

    //    public IEnumerable<Gig> FindGigs()
    //    {
    //        return gigsCollection.AsQueryable();
    //    }

    //    public Gig InsertGig(Gig gig)
    //    {
    //        gigsCollection.InsertOne(gig);
    //        return gig;
    //    }
    //}
    #endregion
}
