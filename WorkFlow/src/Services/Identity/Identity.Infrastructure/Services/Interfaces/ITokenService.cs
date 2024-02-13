using Identity.API.DTOs;
using Identity.Core.Abstractions;
using Identity.Core.Entities;
using Identity.Core.Models;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface ITokenService
	{
		public Task<Result<AuthResult, Error>> GenerateTokenAsync(User user);
		public Task<Result<AuthResult, Error>?> VerifyAndGenerateTokenAsync(TokenRequest tokenRequest);

	}
}
