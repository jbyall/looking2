using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Looking2Console
{
    public abstract class Looking2Migration
    {
        public Looking2Migration()
        {
            this.Name = this.GetType().Name;
            this.Applied = DateTime.Now;
        }
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Applied { get; set; }

        public bool Execute(MongoClient client, IMongoDatabase db)
        {
            // TODO : this logic needs to add the migration to the database
            // TODO : add a status string to the migration class to note if it succeeded or failed
            // TODO : create a backup of the db/collection to rollback if something happens
            if (Logic(client, db))
            {
                Console.WriteLine($"{this.Name} executed.");
                return true;
            }
            return false;
        }

        public abstract bool Logic(MongoClient client, IMongoDatabase db);
    }
}
