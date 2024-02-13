using Identity.Core.Abstractions;
using Identity.Infrastructure.DTOs;
using System.Security.Claims;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface IUserService
	{
		public Task<Result<UserResponse, Error>> GetUserProfileAsync(ClaimsPrincipal claimsPrincipal);
		public Task<Result<bool, Error>> UpdateUserProfileAsync(ClaimsPrincipal claimsPrincipal, UserResponse updatedUser);
		public Task<Result<bool, Error>> ChangePasswordAsync(ClaimsPrincipal claimsPrincipal, PasswordRequest password);
	}
}
