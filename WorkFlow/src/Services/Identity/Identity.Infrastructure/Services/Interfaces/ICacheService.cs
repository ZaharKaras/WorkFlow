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
