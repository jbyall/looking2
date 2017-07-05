using Looking2.Web.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.DataAccess
{
    public interface IRepository<T>
        where T: Entity
    {
        T GetById(string Id);
        IEnumerable<T> GetAll();
        T Add(T entity);
        T Update(T entity);
    }

    public class Repository<T> : IRepository<T>
        where T : Entity
    {
        private MongoClient client;
        private IMongoDatabase db;
        
        public Repository(string collectionName)
        {
            this.client = new MongoClient("mongodb://localhost:27017");
            this.db = client.GetDatabase("looking2");
            this.Collection = db.GetCollection<T>(collectionName);
        }

        public IMongoCollection<T> Collection { get; private set; }

        public T Add(T entity)
        {
            this.Collection.InsertOne(entity);
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return this.Collection.AsQueryable<T>();
        }

        public T GetById(string Id)
        {
            return this.Collection.AsQueryable<T>().Where(o => o.Id == new ObjectId(Id)).SingleOrDefault();
        }

        public T Update(T entity)
        {
            return this.Collection.FindOneAndReplace<T>(o => o.Id == entity.Id, entity);
        }
    }
}
