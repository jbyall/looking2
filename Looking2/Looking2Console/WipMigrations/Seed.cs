using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace Looking2Console.WipMigrations
{
    public class Seed : Looking2Migration, ILooking2Migration
    {
        public Seed() : base()
        {
            this.Created = new DateTime(2017, 9, 10, 10, 42, 0);
        }
        public override bool Logic(MongoClient client, IMongoDatabase db)
        {
            var dbName = db.DatabaseNamespace.DatabaseName;
            client.DropDatabase("dbName");

            return true;
        }
    }
}
