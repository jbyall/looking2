using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Looking2Console
{
    public interface ILooking2Migration
    {
        ObjectId Id { get; set; }
        string Name { get; set; }
        DateTime Created { get; set; }
        DateTime Applied { get; set; }

        bool Logic(MongoClient client, IMongoDatabase db);
    }
}
