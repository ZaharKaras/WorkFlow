using Identity.API.DTOs;
using Identity.Core.Entities;
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
		public async Task<AuthResult> Login(LoginRequest loginRequest)
		{
			var existingUser = await _userManager.FindByEmailAsync(loginRequest.Email);

			if (existingUser is null)
				return (new AuthResult()
				{
					Result = false,
					Errors = new List<string>()
						{
							"Incorrect email"
						}
				});

			var isCorrect = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);

			if (!isCorrect)
				return (new AuthResult()
				{
					Result = false,
					Errors = new List<string>()
						{
							"Incorrect password"
						}
				});

			var token = await _tokenService.GenerateToken(existingUser);

			_logger.LogInformation($"{existingUser.UserName} is login");

			return (token);
		}

		public async Task<AuthResult?> Logout(TokenRequest tokenRequest)
		{
			var refreshTokens = await _refreshTokenService.GetAsync();
			var storeToken = refreshTokens!.FirstOrDefault(x => x.Token == tokenRequest.RefreshToken);

			if (storeToken is null)
				return new AuthResult()
				{
					Result = false,
					Errors = new List<string>()
					{
						"Refresh token doesn't exist"
					}
				};

			storeToken.IsRevoked = true;
			await _refreshTokenService.UpdateAsync(storeToken.Id!, storeToken);

			_logger.LogInformation("Token is updated");

			return new AuthResult()
			{
				Result = true,
				Token = null,
				RefreshToken = null
			};
		}

		public async Task<AuthResult?> RefreshToken(TokenRequest tokenRequest)
		{
			var result = await _tokenService.VerifyAndGenerateToken(tokenRequest);

			return result;
		}

		public async Task<AuthResult> Register(RegistrationRequest registerRequest)
		{
			var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);
			if (existingUser != null)
				return (new AuthResult()
				{
					Result = false,
					Errors = new List<string>()
					{
						"Email already exist"
					}
				});


			var newUser = new User()
			{
				Email = registerRequest.Email,
				UserName = registerRequest.Name
			};

			var isCreated = await _userManager.CreateAsync(newUser, registerRequest.Password);

			if (isCreated.Succeeded)
			{
				var token = await _tokenService.GenerateToken(newUser);

				return (token);
			}

			_logger.LogInformation($"{registerRequest.Name} is registrated");

			return (new AuthResult()
			{
				Result = false,
				Errors = new List<string>()
				{
						isCreated.Errors.ToString()!,
				}
			});
		}
	}
}
