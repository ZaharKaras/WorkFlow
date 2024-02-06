using Identity.Infrastructure.DTOs;
using System.Security.Claims;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface IUserService
	{
		public Task<UserResponse> GetUserProfileAsync(ClaimsPrincipal claimsPrincipal);
		public Task UpdateUserProfileAsync(ClaimsPrincipal claimsPrincipal, UserResponse updatedUser);
		public Task ChangePasswordAsync(ClaimsPrincipal claimsPrincipal, PasswordRequest password);
	}
}
