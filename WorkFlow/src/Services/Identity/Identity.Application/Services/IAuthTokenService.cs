using Identity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
	public interface IAuthTokenService
	{
		Task<string> GenerateIdToken(User user);
		Task<string> GenerateAccessToken(User user);
		Task<string> GenerateRefreshToken();
		Task<Guid> GetUserIdFromToken(string token);
		Task<int> GetRefreshTokenLifetimeInMinutes();
		Task<bool> IsTokenValid(string accessToken, bool validateLifeTime);
	}
}
