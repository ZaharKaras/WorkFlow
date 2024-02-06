using Identity.API.DTOs;
using Identity.Core.Entities;
using Identity.Core.Models;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface ITokenService
	{
		public Task<AuthResult> GenerateToken(User user);
		public Task<AuthResult?> VerifyAndGenerateToken(TokenRequest tokenRequest);

	}
}
