using Identity.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson.IO;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
	public class CacheService : ICacheService
	{
		private IDatabase _db;
		public CacheService()
		{
			var redis = ConnectionMultiplexer.Connect("localhost:6379");
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
