using Looking2.Web.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.DataAccess
{
    public interface ICategoriesRepository : IRepository<Category>
    {
        IEnumerable<Category> GetByType(ListingCategory type);
    }

    public class CategoriesRepository : Repository<Category>, ICategoriesRepository
    {
        public CategoriesRepository() : base("categories")
        {
        }

        public IEnumerable<Category> GetByType(ListingCategory type)
        {
            return this.Collection.AsQueryable().Where(c => c.Type == type);
        }
    }

    public interface IEventsRepository : IRepository<EventListing>
    {

    }

    public class EventsRepository : Repository<EventListing>, IEventsRepository
    {
        public EventsRepository() : base("events")
        {
        }
    }

    public interface IBusinessRepository : IRepository<BusinessListing> { }
    public class BusinessRepository : Repository<BusinessListing>, IBusinessRepository
    {
        public BusinessRepository() : base("businesses")
        {
        }
    }

    public interface IEventFormsRepo : IRepository<EventFormData>
    {
        EventFormData GetByName(string name);
    }

    public class EventFormsRepository : Repository<EventFormData>, IEventFormsRepo
    {
        public EventFormsRepository() : base("eventforms")
        {

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
        public BusinessFormsRepository() : base("businessforms")
        {

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
