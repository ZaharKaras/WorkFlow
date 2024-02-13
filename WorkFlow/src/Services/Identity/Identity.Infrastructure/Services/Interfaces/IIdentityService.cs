using Identity.API.DTOs;
using Identity.Core.Abstractions;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface IIdentityService
	{
		public Task<Result<TokenResponse, Error>> RegisterAsync(RegistrationRequest registerRequest);
		public Task<Result<TokenResponse, Error>> LoginAsync(LoginRequest loginRequest);
		public Task<Result<TokenResponse, Error>?> RefreshTokenAsync(TokenRequest tokenRequest);
		public Task<Result<bool, Error>?> LogoutAsync(TokenRequest tokenRequest);

	}
}
