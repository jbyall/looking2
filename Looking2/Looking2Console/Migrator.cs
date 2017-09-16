using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Looking2Console
{
    public class Migrator
    {
        private MongoClient _client;
        private IMongoDatabase _db;
        private const string migrationsDirectory = "Migrations";
        private IMongoCollection<Looking2Migration> _migrationsCollection;

        public Migrator(MongoClient client, IMongoDatabase db)
        {
            _client = client;
            _db = db;
            _migrationsCollection = db.GetCollection<Looking2Migration>("migrations");
        }

        public void Migrate()
        {
            Console.WriteLine($"Connected To: {_client.Settings.Server.Host}");
            Console.WriteLine($"Continue to apply the following migrations:");
            foreach (var item in GetMigrationSummary())
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
            ApplyMigrations();
            Console.WriteLine("Done");
            Console.Read();
        }

        public List<string> GetMigrationSummary()
        {
            return getNewMigrations().Select(m => m.Name).ToList();
        }

        private List<Type> getNewMigrations()
        {
            var result = new List<Type>();
            var migrationFiles = getAllMigrations();
            var appliedMigrations = getAppliedMigrations();
            foreach (var item in migrationFiles)
            {
                if (!appliedMigrations.Contains(item.Name))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        private List<Type> getAllMigrations()
        {
            var assy = typeof(Looking2Migration).GetTypeInfo().Assembly;
            return assy.GetTypes().Where(t => t.Namespace == "Looking2Console.Migrations" && !t.IsNested).ToList();
        }

        private List<string> getAppliedMigrations()
        {
            return _migrationsCollection.AsQueryable().Select(m => m.Name).ToList();
        }

        public bool ApplyMigrations()
        {
            var migrations = getNewMigrations();
            foreach (var item in migrations)
            {
                var test = item.GetType();
                var runner = Activator.CreateInstance(item);
                MethodInfo method = item.GetMethod("Execute");
                var result = method.Invoke(runner, new object[] { _client, _db });
            }
            return true;
        }
    }
}
