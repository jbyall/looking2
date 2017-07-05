using Looking2.Web.Domain;
using Looking2.Web.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.DataAccess
{
    public class Looking2DbContext
    {
        private string connectionString;

        public Looking2DbContext(string connection)
        {
            this.connectionString = connection;
            this.Client = new MongoClient(connectionString);
            this.Db = Client.GetDatabase("looking2");
            this.Events = Db.GetCollection<EventListing>("events");
            this.Businesses = Db.GetCollection<BusinessListing>("businesses");
            this.Categories = Db.GetCollection<Category>("categories");
            this.EventForms = Db.GetCollection<EventFormData>("eventforms");
            this.BusinessForms = Db.GetCollection<BusinessFormData>("businessforms");
        }

        #region properties
        public MongoClient Client { get; private set; }
        public IMongoDatabase Db { get; private set; }
        public IMongoCollection<EventListing> Events { get; private set; }
        public IMongoCollection<BusinessListing> Businesses { get; private set; }
        public IMongoCollection<Category> Categories { get; private set; }
        public IMongoCollection<EventFormData> EventForms { get; private set; }
        public IMongoCollection<BusinessFormData> BusinessForms { get; private set; }
        #endregion
    }
}
