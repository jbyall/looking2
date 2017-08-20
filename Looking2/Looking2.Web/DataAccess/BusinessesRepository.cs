using Looking2.Web.Domain;
using Looking2.Web.Services;
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
    public interface IBusinessRepository : IRepository<BusinessListing>
    {
        List<BusinessListing> SearchListings(SearchCriteria criteria);
        //List<BusinessListing> SearchDescriptionFields(string text, int maxResults = 100);
        //List<BusinessListing> SearchTitleAndDescription(string title, string description, SearchOperator searchType, int maxResults = 100);
    }
    public class BusinessRepository : Repository<BusinessListing>, IBusinessRepository
    {
        private IListingCleaner cleaner;

        public BusinessRepository(IOptions<DbSettings> settings, IListingCleaner _cleaner) : base(settings)
        {
            this.cleaner = _cleaner;
            this.Collection = Db.GetCollection<BusinessListing>(settings.Value.BusinessesCollection);
        }

        public override BusinessListing Add(BusinessListing entity)
        {
            entity.Listify(cleaner);
            return base.Add(entity);
        }

        public List<BusinessListing> SearchListings(SearchCriteria criteria)
        {
            // Category only search
            if (criteria.CategoryFilter > 0 && criteria.DetailFilters.Count < 1 && string.IsNullOrWhiteSpace(criteria.LocationFilter))
            {
                return this.Collection.Find(new BsonDocument("Category", criteria.CategoryFilter)).ToList();
            }

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
            if (!string.IsNullOrWhiteSpace(criteria.LocationFilter) && criteria.DetailFilters.Count < 1 && criteria.CategoryFilter < 1)
            {
                return this.Collection.Find<BusinessListing>(locationFilter).ToList();
            }

            // Execute detail query if no location
            if (string.IsNullOrWhiteSpace(criteria.LocationFilter) && criteria.DetailFilters.Count > 0 && criteria.CategoryFilter < 1)
            {
                return this.Collection.Find<BusinessListing>(detailsFilter).ToList();
            }

            var andFilters = new List<FilterDefinition<BusinessListing>>();
            if (criteria.CategoryFilter > 0)
            {
                andFilters.Add(new BsonDocument("Category", criteria.CategoryFilter));
            }

            if (criteria.DetailFilters.Count > 0)
            {
                andFilters.Add(detailsFilter);
            }

            if (!string.IsNullOrWhiteSpace(criteria.LocationFilter))
            {
                andFilters.Add(locationFilter);
            }

            // Build and execute $and query if both exist
            var andQueryBuilder = Builders<BusinessListing>.Filter;
            var detailQuery = andQueryBuilder.And(andFilters);
            //var detailQuery = FilterDefinition<BusinessListing>.Empty;
            //if (criteria.CategoryFilter > 0)
            //{
            //    var categoryFilter = new BsonDocument("Category", criteria.CategoryFilter);
            //    detailQuery = andQueryBuilder.And(detailsFilter, locationFilter, categoryFilter);
            //}
            //else
            //{
            //    detailQuery = andQueryBuilder.And(detailsFilter, locationFilter);
            //}
            //var detailQuery = andQueryBuilder.And(detailsFilter, locationFilter);
            //var detailQuery = andQueryBuilder.And(andFilters.ToArray());
            return this.Collection.Find<BusinessListing>(detailQuery).ToList();

        }

        //public List<BusinessListing> SearchDescriptionFields(string text, int maxResults = 100)
        //{
        //    var filter = new BsonDocument("Descriptions", new BsonDocument("$regex", string.Format("(?i){0}", text)));
        //    return this.Collection.Find<BusinessListing>(filter)
        //                    .Limit(maxResults)
        //                    .ToList();
        //}

        //public List<BusinessListing> SearchTitleAndDescription(string title, string description, SearchOperator searchType, int maxResults = 100)
        //{
        //    string searchOp = "";
        //    switch (searchType)
        //    {
        //        case SearchOperator.And:
        //            searchOp = "$and";
        //            break;
        //        case SearchOperator.Or:
        //            searchOp = "$or";
        //            break;
        //        default:
        //            break;
        //    }
        //    var filter = new BsonDocument(searchOp, new BsonArray
        //    {
        //        new BsonDocument("Titles", new BsonDocument("$regex", string.Format("(?i){0}", title))),
        //        new BsonDocument("Descriptions", new BsonDocument("$regex", string.Format("(?i){0}", description))),
        //    });
        //    return this.Collection.Find<BusinessListing>(filter)
        //                    .Limit(maxResults)
        //                    .ToList();
        //}


        // TODO:
        //1. Add an exists method
        //2. Try to combine BusinessRepository and EventRepository into 1 generic
    }
}
