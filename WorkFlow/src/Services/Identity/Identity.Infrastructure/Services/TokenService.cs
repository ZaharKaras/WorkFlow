using Identity.API.DTOs;
using Identity.Core.Abstractions;
using Identity.Core.Entities;
using Identity.Core.Errors;
using Identity.Core.Models;
using Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Infrastructure.Services
{
	public class TokenService : ITokenService
	{
		private readonly UserManager<User> _userManager;
		private readonly IConfiguration _config;
		private readonly IRefreshTokenService _refreshTokenService;
		private readonly TokenValidationParameters _tokenValidationParameters;

		public TokenService(
			UserManager<User> userManager,
			IConfiguration config,
			IRefreshTokenService refreshTokenService,
			TokenValidationParameters tokenValidationParameters)
		{
			_userManager = userManager;
			_config = config;
			_refreshTokenService = refreshTokenService;
			_tokenValidationParameters = tokenValidationParameters;
		}

		private SecurityToken GenerateJwtToken(User user)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();

			var key = Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings:Key").Value!);

			var tokenDescriptor = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
					new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
					new Claim(JwtRegisteredClaimNames.Email, user.Email!),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
				}),

				Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_config.GetSection("JwtSettings:ExpiryTimeFrame").Value!)),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
			};

			 return jwtTokenHandler.CreateToken(tokenDescriptor);
		}

		private string RandomStringGeneration(int lenght)
		{
			var randomNumber = new byte[lenght];

			using var generator = RandomNumberGenerator.Create();
			generator.GetBytes(randomNumber);

			return Convert.ToBase64String(randomNumber);
		}

		private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
		{
			var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

			return dateTimeVal;
		}

		public async Task<Result<AuthResult, Error>?> VerifyAndGenerateTokenAsync(TokenRequest tokenRequest)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();

			var tokenInVerification =
				jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);

			if (validatedToken is JwtSecurityToken jwtSecurityToken)
			{
				var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
					StringComparison.InvariantCultureIgnoreCase);

				if (result is false)
					return null;
			}

			var utcExpiryDate = long.Parse(tokenInVerification.Claims.
				FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)!.Value);

			var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

			if (expiryDate > DateTime.Now)
			{
				return TokenErrors.ExpiredToken;
			}

			var refreshTokens = await _refreshTokenService.GetAsync();
			var storeToken = refreshTokens!.FirstOrDefault(x => x.Token == tokenRequest.RefreshToken);

			if (storeToken is null || storeToken.IsUsed || storeToken.IsRevoked)
			{
				return TokenErrors.InvalidTokens;
			}

			var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)!.Value;

			if (storeToken.JwtId != jti || storeToken.ExpiryDate < DateTime.UtcNow)
			{
				return TokenErrors.InvalidTokens;
			}

			var dbUser = await _userManager.FindByIdAsync(storeToken.UserId!);

			if (dbUser is null)
			{
				return UserErrors.UserNotFound;
			}

			var token = GenerateJwtToken(dbUser);
			var jwtToken = jwtTokenHandler.WriteToken(token);

			return new AuthResult()
			{
				Token = jwtToken,
				RefreshToken = tokenRequest.RefreshToken
			};
		}

		public async Task<Result<AuthResult, Error>> GenerateTokenAsync(User user)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();

			var token = GenerateJwtToken(user);
			var jwtToken = jwtTokenHandler.WriteToken(token);

			var refreshToken = new RefreshToken()
			{
				JwtId = token.Id,
				Token = RandomStringGeneration(8),
				AddedDate = DateTime.UtcNow,
				ExpiryDate = DateTime.UtcNow.AddMonths(1),
				IsRevoked = false,
				IsUsed = false,
				UserId = user.Id.ToString()
			};

			await _refreshTokenService.CreateAsync(refreshToken);

			return new AuthResult()
			{
				RefreshToken = refreshToken.Token,
				Token = jwtToken
			};
		}
	}
}
