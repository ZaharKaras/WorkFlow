using Identity.Core.Entities;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface IRefreshTokenService
	{
		public Task<List<RefreshToken>?> GetAsync();
		public Task<RefreshToken?> GetAsync(string id);
		public Task CreateAsync(RefreshToken token);
		public Task UpdateAsync(string id, RefreshToken token);
		public Task DeleteAsync(string id);
	}
}
