using Identity.API.DTOs;
using Identity.Core.Models;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface IIdentityService
	{
		public Task<AuthResult> Register(RegistrationRequest registerRequest);
		public Task<AuthResult> Login(LoginRequest loginRequest);
		public Task<AuthResult?> RefreshToken(TokenRequest tokenRequest);
		public Task<AuthResult?> Logout(TokenRequest tokenRequest);

	}
}
