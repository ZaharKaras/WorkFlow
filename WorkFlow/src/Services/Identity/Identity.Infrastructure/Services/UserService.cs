using AutoMapper;
using Identity.Core.Entities;
using Identity.Infrastructure.DTOs;
using Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Identity.Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;
		private readonly ILogger<UserService> _logger;
		private readonly IMapper _mapper;

		public UserService(UserManager<User> userManager, ILogger<UserService> logger, IMapper mapper)
		{
			_userManager = userManager;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task ChangePasswordAsync(ClaimsPrincipal claimsPrincipal, PasswordRequest password)
		{
			var user = await _userManager.GetUserAsync(claimsPrincipal);

			if (user == null)
			{
				throw new Exception("User does not exist");
			}

			var changePasswordResult = await _userManager.ChangePasswordAsync(user, password.OldPassword, password.NewPassword);

			if (!changePasswordResult.Succeeded)
			{
				foreach (var error in changePasswordResult.Errors)
				{
					_logger.LogError(error.Description);
				}

				throw new Exception("Password change failed");
			}

			_logger.LogInformation("User changed their password successfully.");
		}
	

		public async Task<UserResponse> GetUserProfileAsync(ClaimsPrincipal claimsPrincipal)
		{
			var user = await _userManager.GetUserAsync(claimsPrincipal);

			if (user == null)
				throw new Exception("User does not exist");

			var result = _mapper.Map<UserResponse>(user);

			return result!;
		}

		public async Task UpdateUserProfileAsync(ClaimsPrincipal claimsPrincipal, UserResponse updatedUser)
		{
			var user = await _userManager.GetUserAsync(claimsPrincipal);

			if (user == null)
				throw new Exception("User does not exist");

			user = _mapper.Map<User>(user);

			_mapper.Map(updatedUser, user);

			await _userManager.UpdateAsync(user!);
		}
	}
}
