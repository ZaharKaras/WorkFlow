using Identity.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace Identity.Infrastructure.Services
{
	public class CacheService : ICacheService
	{
		private readonly IDatabase _db;

		public CacheService(IConfiguration config)
		{
			var redis = ConnectionMultiplexer.Connect(config.GetSection("Redis:ConnectionString").Value!);
			_db = redis.GetDatabase();
		}

		public async Task<T?> GetData<T>(string key) where T : class
		{
			var value = await _db.StringGetAsync(key);
			if (!string.IsNullOrEmpty(value))
				return JsonSerializer.Deserialize<T>(value!);

			return default;
		}

		public async Task<bool> RemoveData(string key)
		{
			bool isKeyExist = await _db.KeyExistsAsync(key);
			if (isKeyExist == true)
				return _db.KeyDelete(key);

			return false;
		}

		public async Task<bool> SetData<T>(string key, T value) where T : class
		{
			var cacheValue = JsonSerializer.Serialize<T>(value);

			return await _db.StringSetAsync(key, cacheValue);
		}
	}
}
