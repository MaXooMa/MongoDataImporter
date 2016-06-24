using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;


namespace MongoDataImporter.DbConnectors
{
    /// <summary>
    /// A mongoDb implementation for the IDbConnector
    /// </summary>
    internal class MongoConnector: IDbConnector
    {

        private string ConnString { get; set; }
        private string DbName { get; set; }
        private string CollectionName { get; set; }

        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public MongoConnector(string connectionString, string dbName, string collectionName)
        {
            this.ConnString = connectionString;
            this.DbName = dbName;
            this.CollectionName = collectionName;
            Connect();
        }

        public void Connect()
        {
            _client = new MongoClient(ConnString);
            _database = _client.GetDatabase(DbName);
        }

        public void InsertDocument(object document)
        {
            var collection = _database.GetCollection<BsonDocument>(CollectionName);
            collection.InsertOne(document.ToBsonDocument());
        }

    }
}
