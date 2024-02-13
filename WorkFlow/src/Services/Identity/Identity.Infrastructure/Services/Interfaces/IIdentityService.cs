using Identity.API.DTOs;
using Identity.Core.Abstractions;
using Identity.Core.Models;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface IIdentityService
	{
		public Task<Result<AuthResult, Error>> RegisterAsync(RegistrationRequest registerRequest);
		public Task<Result<AuthResult, Error>> LoginAsync(LoginRequest loginRequest);
		public Task<Result<AuthResult, Error>?> RefreshTokenAsync(TokenRequest tokenRequest);
		public Task<Result<bool, Error>?> LogoutAsync(TokenRequest tokenRequest);

	}
}
