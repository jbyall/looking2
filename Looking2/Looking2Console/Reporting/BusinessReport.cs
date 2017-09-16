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
    public class BusinessReport
    {
        private MongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<BusinessListing> _businesses;

        public BusinessReport(string connectionString)
        {
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase("looking2");
            _businesses = _db.GetCollection<BusinessListing>("businesses");
        }

        public BusinessCategoryResult GetCategoriesInBusinessType(BusinessType bizType)
        {
            var result = new BusinessCategoryResult();
            result.ListingType = bizType.ToString();
            var listings = _businesses.Find<BusinessListing>(b => b.BusinessType == bizType).ToList();
            var categories = listings.Select(l => l.Categories).Distinct().ToList();
            foreach (var item in categories)
            {
                var catCount = listings.Where(l => l.Categories == item).Count();
                //result.TypeCount.Add(item, catCount);
            }
            return result;
        }
    }
}
