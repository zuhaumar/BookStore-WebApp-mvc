using MongoDB.Driver;
using Ecommerce.Models;

namespace Ecommerce.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Book> Books => _database.GetCollection<Book>("Books"); 

    }

}