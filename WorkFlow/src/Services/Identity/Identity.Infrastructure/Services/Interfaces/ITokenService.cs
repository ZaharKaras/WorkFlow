using Identity.API.DTOs;
using Identity.Core.Abstractions;
using Identity.Core.Entities;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface ITokenService
	{
		public Task<Result<TokenResponse, Error>> GenerateTokenAsync(User user);
		public Task<Result<TokenResponse, Error>?> VerifyAndGenerateTokenAsync(TokenRequest tokenRequest);

	}
}
