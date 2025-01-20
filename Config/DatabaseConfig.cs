using MongoDB.Driver;

namespace WebServerExample.Config
{
  public class DatabaseConfig
  {
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;

    public DatabaseConfig(string connectionString, string databaseName)
    {
      _client = new MongoClient(connectionString);
      _database = _client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
      return _database.GetCollection<T>(collectionName);
    }
  }
}
