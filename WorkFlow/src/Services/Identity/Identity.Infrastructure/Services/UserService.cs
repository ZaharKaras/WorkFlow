using AutoMapper;
using Identity.Core.Abstractions;
using Identity.Core.Entities;
using Identity.Core.Errors;
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

		public async Task<Result<bool, Error>> ChangePasswordAsync(ClaimsPrincipal claimsPrincipal, PasswordRequest password)
		{
			var user = await _userManager.GetUserAsync(claimsPrincipal);

			if (user is null)
			{
				return UserErrors.UserNotFound;
			}

			var changePasswordResult = await _userManager.ChangePasswordAsync(user, password.OldPassword, password.NewPassword);

			if (!changePasswordResult.Succeeded)
			{
				foreach (var error in changePasswordResult.Errors)
				{
					_logger.LogError(error.Description);
				}

				return UserErrors.PasswordChangeError;
			}

			_logger.LogInformation("User changed password successfully.");

			return true;
		}
	

		public async Task<Result<UserResponse, Error>> GetUserProfileAsync(ClaimsPrincipal claimsPrincipal)
		{
			var user = await _userManager.GetUserAsync(claimsPrincipal);

			if (user is null)
			{
				return UserErrors.UserNotFound;
			}

			var result = _mapper.Map<UserResponse>(user);

			return result!;
		}

		public async Task<Result<bool, Error>> UpdateUserProfileAsync(ClaimsPrincipal claimsPrincipal, UserResponse updatedUser)
		{
			var user = await _userManager.GetUserAsync(claimsPrincipal);

			if (user is null)
			{
				return UserErrors.UserNotFound;
			}

			user = _mapper.Map<User>(user);

			_mapper.Map(updatedUser, user);

			await _userManager.UpdateAsync(user!);

			return true;
		}
	}
}
