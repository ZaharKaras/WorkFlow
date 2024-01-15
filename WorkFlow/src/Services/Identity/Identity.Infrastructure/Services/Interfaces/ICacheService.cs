using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface ICacheService
	{
		Task<T?> GetData<T>(string key)
			where T : class;
		Task<bool> SetData<T>(string key,  T value)
			where T : class;
		Task<bool> RemoveData(string key);
	}
}
