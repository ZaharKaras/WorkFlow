using Identity.Core.Entities;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface IRefreshTokenService
	{
		public Task<RefreshToken?> GetByValueAsync(string value);
		public Task CreateAsync(RefreshToken token);
		public Task UpdateAsync(string id, RefreshToken token);
		public Task DeleteAsync(string value);
	}
}
