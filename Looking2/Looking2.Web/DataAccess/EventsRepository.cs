using Looking2.Web.Domain;
using Looking2.Web.Services;
using Looking2.Web.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Looking2.Web.DataAccess
{
    public interface IEventsRepository : IRepository<EventListing>
    {
        List<EventListing> SearchDescriptionFields(string text, int maxResults = 100);
        List<EventListing> SearchTitleAndDescription(string title, string description, SearchOperator searchType, int maxResults = 100);
        List<EventListing> SearchListings(SearchCriteria criteria);
    }
    public class EventsRepository : Repository<EventListing>, IEventsRepository
    {
        private IListingCleaner cleaner;

        public EventsRepository(IOptions<DbSettings> settings, IListingCleaner _cleaner) : base(settings)
        {
            this.cleaner = _cleaner;
            this.Collection = Db.GetCollection<EventListing>(settings.Value.EventsCollection);
        }

        public override EventListing Add(EventListing entity)
        {
            entity.Listify(cleaner);
            return base.Add(entity);
        }

        public List<EventListing> SearchListings(SearchCriteria criteria)
        {
            //Construct description, title, venue search
            var detailFilterArray = new BsonArray();
            foreach (var item in criteria.DetailFilters)
            {
                detailFilterArray.Add(new BsonDocument(item.FieldName, new BsonDocument("$regex", string.Format("(?i){0}", item.Value))));
            }
            var detailsFilter = new BsonDocument("$or", detailFilterArray);

            //construct location search
            var locationFilter = new BsonDocument("Location", new BsonDocument("$regex", string.Format("(?i){0}", criteria.LocationFilter)));

            // Execute just location if no detail filters exist
            if (!string.IsNullOrWhiteSpace(criteria.LocationFilter) && criteria.DetailFilters.Count < 1)
            {
                return this.Collection.Find<EventListing>(locationFilter).ToList();
            }

            // Execute detail query if no location
            if (string.IsNullOrWhiteSpace(criteria.LocationFilter) && criteria.DetailFilters.Count > 0)
            {
                return this.Collection.Find<EventListing>(detailsFilter).ToList();
            }

            // Build and execute $ and query if both exist
            var andQueryBuilder = Builders<EventListing>.Filter;
            var detailQuery =  andQueryBuilder.And(detailsFilter, locationFilter);
            return this.Collection.Find<EventListing>(detailQuery).ToList();

        }

        public List<EventListing> SearchDescriptionFields(string text, int maxResults = 100)
        {
            var filter = new BsonDocument("Descriptions", new BsonDocument("$regex", string.Format("(?i){0}", text)));
            return this.Collection.Find<EventListing>(filter)
                            .Limit(maxResults)
                            .ToList();
        }

        public List<EventListing> SearchTitleAndDescription(string title, string description, SearchOperator searchType, int maxResults = 100)
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
                new BsonDocument("Venue", new BsonDocument("$regex", string.Format("(?i){0}", title))),
                new BsonDocument("Descriptions", new BsonDocument("$regex", string.Format("(?i){0}", description))),
                
            });
            return this.Collection.Find<EventListing>(filter)
                            .Limit(maxResults)
                            .ToList();
        }
    }
}
