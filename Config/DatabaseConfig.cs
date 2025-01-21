using Microsoft.AspNetCore.Mvc.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;
using WebServerExample.Models;

namespace WebServerExample.Config
{
  public class DatabaseConfig
  {
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;
    public IMongoCollection<BsonDocument> _collection;

    public DatabaseConfig(string connectionString, string databaseName, string collectionName, long maxByteSize)
    {
      _client = new MongoClient(connectionString);
      _database = _client.GetDatabase(databaseName);
      _collection = GetCollection<BsonDocument>(collectionName, maxByteSize);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName, long maxByteSize)
    {
      EnsureCappedCollection(collectionName, maxByteSize);
      return _database.GetCollection<T>(collectionName);
    }

    public void EnsureCappedCollection(string collectionName, long maxByteSize)
    {
      // Check if the collection already exists
      var collectionNames = _database.ListCollectionNames().ToList();
      if (!collectionNames.Contains(collectionName))
      {
        var options = new CreateCollectionOptions
        {
          Capped = true,
          MaxSize = maxByteSize,
        };

        // Create the capped collection if it doesn't exist
        _database.CreateCollection(collectionName, options);
      }
    }
  }

}
