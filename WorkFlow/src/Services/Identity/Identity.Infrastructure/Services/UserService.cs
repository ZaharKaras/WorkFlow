﻿using AutoMapper;
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
				changePasswordResult.Errors.Select(error => error.Description)
					.ToList()
					.ForEach(description => _logger.LogError(description));

				return UserErrors.PasswordChangeError;
			}

			_logger.LogInformation("User changed password successfully.");

			return false;
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

		public async Task<Result<UserResponse, Error>> UpdateUserProfileAsync(ClaimsPrincipal claimsPrincipal, UserResponse updatedUser)
		{
			var user = await _userManager.GetUserAsync(claimsPrincipal);

			if (user is null)
			{
				return UserErrors.UserNotFound;
			}

			user = _mapper.Map<User>(user);

			_mapper.Map(updatedUser, user);

			await _userManager.UpdateAsync(user!);

			var result = _mapper.Map<UserResponse>(user);

			return result!;
		}
	}
}
