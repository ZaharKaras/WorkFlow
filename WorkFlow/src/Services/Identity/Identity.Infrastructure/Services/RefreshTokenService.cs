using Identity.Core.Entities;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
	public class RefreshTokenService : IRefreshTokenService
	{
		private readonly IMongoCollection<RefreshToken> _tokensCollection;

		public RefreshTokenService(
			IOptions<DataBaseSettings> tokenStoreDatabaseSettings)
		{
			var mongoClient = new MongoClient(
				tokenStoreDatabaseSettings.Value.ConnectionString);

			var mongoDatabase = mongoClient.GetDatabase(
				tokenStoreDatabaseSettings.Value.DataBaseName);

			_tokensCollection = mongoDatabase.GetCollection<RefreshToken>(
				tokenStoreDatabaseSettings.Value.CollectionName);
		}


		public async Task<List<RefreshToken>?> GetAsync() =>
	   await _tokensCollection.Find(_ => true).ToListAsync();

		public async Task<RefreshToken?> GetAsync(string id) =>
			await _tokensCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

		public async Task CreateAsync(RefreshToken newBook) =>
			await _tokensCollection.InsertOneAsync(newBook);

		public async Task UpdateAsync(string id, RefreshToken updatedBook) =>
			await _tokensCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

		public async Task DeleteAsync(string id) =>
			await _tokensCollection.DeleteOneAsync(x => x.Id == id);
	}
}
