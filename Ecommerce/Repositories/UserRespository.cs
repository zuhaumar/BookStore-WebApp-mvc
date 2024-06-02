using MongoDB.Driver;
using Ecommerce.Models;
using Ecommerce.Data;

namespace Ecommerce.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(MongoDbContext context)
        {
            _userCollection = context.Users;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userCollection.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            return await _userCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateUser(User user)
        {
            await _userCollection.InsertOneAsync(user);
        }

        public async Task<bool> UpdateUser(string id, User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var update = Builders<User>.Update
                .Set(u => u.Firstname, user.Firstname)
                .Set(u => u.Lastname, user.Lastname)
                .Set(u => u.Username, user.Username)
                .Set(u => u.Email, user.Email)
                .Set(u => u.Password, user.Password);
            var result = await _userCollection.UpdateOneAsync(filter, update);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var result = await _userCollection.DeleteOneAsync(user => user.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}