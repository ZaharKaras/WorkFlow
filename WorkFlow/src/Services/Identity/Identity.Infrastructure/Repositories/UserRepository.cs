using Identity.Core.Entities;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly IMongoCollection<User> _userCollection;

		public UserRepository(IOptions<DataBaseSettings> settings)
		{
			var mongoClient = new MongoClient(settings.Value.ConnectionString);
			var mongoDb = mongoClient.GetDatabase(settings.Value.DataBaseName);
			_userCollection = mongoDb.GetCollection<User>(settings.Value.CollectionName);
		}

		public async Task CreateUser(User user)
		{
			await _userCollection.InsertOneAsync(user);
		}

		public async Task<User> GetUserByEmail(string email)
		{
			return await _userCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
		}

		public async Task<User> GetUserByUserId(Guid userId)
		{
			return await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();
		}

		public async Task RemoveUser(Guid userId)
		{
			await _userCollection.DeleteOneAsync(x => x.Id == userId);
		}

		public async Task UpdateUser(User user)
		{
			await _userCollection.ReplaceOneAsync(x => x.Id == user.Id, user);
		}
	}
}
