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
    public interface IRepository<T>
        where T: Entity
    {
        IFindFluent<T,T> Find(Expression<Func<T,bool>> predicate);
        T GetById(string Id);
        IEnumerable<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        IMongoDatabase Db { get; }
        IMongoCollection<T> Collection { get; }
        void Delete(string id);
    }

    public class Repository<T> : IRepository<T>
        where T : Entity
    {

        public Repository(IOptions<DbSettings> settings)
        {
            this.Client = new MongoClient(settings.Value.ConnectionString);
            this.Db = Client.GetDatabase(settings.Value.DatabaseName);
        }

        public MongoClient Client { get; private set; }
        public IMongoDatabase Db { get; private set; }
        public IMongoCollection<T> Collection { get; set; }

        public IFindFluent<T,T> Find(Expression<Func<T,bool>> predicate)
        {
            return this.Collection.Find<T>(predicate);
        }

        public virtual T Add(T entity)
        {
            entity.Created = DateTime.Now;
            entity.Modified = DateTime.Now;
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
            entity.Modified = DateTime.Now;
            return this.Collection.FindOneAndReplace<T>(o => o.Id == entity.Id, entity);
        }

        public void Delete(string id)
        {
            this.Collection.DeleteOne(new BsonDocument("_id", new ObjectId(id)));
        }
    }
}
