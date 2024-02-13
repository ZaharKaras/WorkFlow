using Identity.API.DTOs;
using Identity.Core.Abstractions;
using Identity.Core.Entities;
using Identity.Core.Errors;
using Identity.Core.Models;
using Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Identity.Infrastructure.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly UserManager<User> _userManager;
		private readonly IRefreshTokenService _refreshTokenService;
		private readonly ITokenService _tokenService;
		private readonly ILogger<IdentityService> _logger;

		public IdentityService(
			UserManager<User> userManager,
			IRefreshTokenService refreshTokenService,
			ITokenService tokenService,
			ILogger<IdentityService> logger)
		{
			_userManager = userManager;
			_refreshTokenService = refreshTokenService;
			_tokenService = tokenService;
			_logger = logger;
		}

		public async Task<Result<AuthResult, Error>> LoginAsync(LoginRequest loginRequest)
		{
			var existingUser = await _userManager.FindByEmailAsync(loginRequest.Email);

			if (existingUser is null)
			{
				return UserErrors.UserNotFound;
			}

			var isCorrect = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);

			if (!isCorrect)
			{
				return UserErrors.IncorrectPassword;
			}

			var token = await _tokenService.GenerateTokenAsync(existingUser);

			_logger.LogInformation($"{existingUser.UserName} is log in");

			return (token);
		}

		public async Task<Result<bool, Error>> LogoutAsync(TokenRequest tokenRequest)
		{
			var refreshTokens = await _refreshTokenService.GetAsync();
			var storeToken = refreshTokens!.FirstOrDefault(x => x.Token == tokenRequest.RefreshToken);

			if (storeToken is null)
			{
				return TokenErrors.RefreshTokenNotFound;
			}

			storeToken.IsRevoked = true;
			await _refreshTokenService.UpdateAsync(storeToken.Id!, storeToken);

			_logger.LogInformation("Token is updated");

			return true;
		}

		public async Task<Result<AuthResult, Error>> RefreshTokenAsync(TokenRequest tokenRequest)
		{
			return await _tokenService.VerifyAndGenerateTokenAsync(tokenRequest);
		}

		public async Task<Result<AuthResult, Error>> RegisterAsync(RegistrationRequest registerRequest)
		{
			var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);
			if (existingUser != null)
			{
				return UserErrors.UserAlreadyExists;
			}

			var newUser = new User()
			{
				Email = registerRequest.Email,
				UserName = registerRequest.Name
			};

			var isCreated = await _userManager.CreateAsync(newUser, registerRequest.Password);

			if (isCreated.Succeeded)
			{
				_logger.LogInformation($"{registerRequest.Name} is registrated");

				var token = await _tokenService.GenerateTokenAsync(newUser);

				return (token);
			}

			return UserErrors.UserCreationFailed;
		}
	}
}
