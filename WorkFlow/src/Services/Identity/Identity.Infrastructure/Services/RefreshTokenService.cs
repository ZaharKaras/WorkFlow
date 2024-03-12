using Identity.Core.Entities;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Identity.Infrastructure.Services
{
	public class RefreshTokenService : IRefreshTokenService
	{
		private readonly IMongoCollection<RefreshToken> _tokensCollection;
		private readonly IDistributedCache _cache;

		public RefreshTokenService(
			IOptions<DataBaseSettings> tokenStoreDatabaseSettings,
			IDistributedCache cache,
			IMongoClient client)
		{
			var mongoDatabase = client.GetDatabase(
				tokenStoreDatabaseSettings.Value.DataBaseName);

			_tokensCollection = mongoDatabase.GetCollection<RefreshToken>(
				tokenStoreDatabaseSettings.Value.CollectionName);

			_cache = cache;
		}

		public async Task<RefreshToken?> GetByValueAsync(string value)
		{
			string key = $"RefreshToken-{value}";

			string? cachedToken = await _cache.GetStringAsync(key);

			if (string.IsNullOrEmpty(cachedToken))
			{
				var token = await _tokensCollection.Find(x => x.Token == value).FirstOrDefaultAsync();

                if (token != null)
                {
					await _cache.SetStringAsync(key, JsonConvert.SerializeObject(token));
				}

				return token;
			}

			return JsonConvert.DeserializeObject<RefreshToken>(cachedToken)!;
		}

		public async Task CreateAsync(RefreshToken token)
		{
			await _tokensCollection.InsertOneAsync(token);
		}

		public async Task UpdateAsync(string id, RefreshToken updatedToken)
		{
			await _tokensCollection.ReplaceOneAsync(x => x.Id == id, updatedToken);
		}

		public async Task DeleteAsync(string value)
		{
			await _cache.RemoveAsync($"RefreshToken-{value}");

			await _tokensCollection.DeleteOneAsync(x => x.Token == value);
		}
	}
}
