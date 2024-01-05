using Identity.API.DTOs;
using Identity.Core.Entities;
using Identity.Core.Models;
using Identity.Infrastructure.Services;
using Identity.Infrastructure.Services.Interfaces;
using Identity.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IdentityController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IConfiguration _config;
		private readonly IRefreshTokenService _refreshTokenService;
		private readonly TokenValidationParameters _tokenValidationParameters;

		public IdentityController(
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

		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequest requestDto)
		{
			if (ModelState.IsValid)
			{
				var user_exist = await _userManager.FindByEmailAsync(requestDto.Email);
				if (user_exist != null)
					return BadRequest(new AuthResult()
					{
						Result = false,
						Errors = new List<string>()
						{
							"Email already exist"
						}
					});


				var new_user = new User()
				{
					Email = requestDto.Email,
					UserName = requestDto.Name
				};

				var is_created = await _userManager.CreateAsync(new_user, requestDto.Password);

				if (is_created.Succeeded)
				{
					var token = await GenerateJwtToken(new_user);

					return Ok(token);
				}

				return BadRequest(new AuthResult()
				{
					Result = false,
					Errors = new List<string>()
					{
						"Server error"
					}
				});

			}

			return BadRequest(ModelState);
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] DTOs.LoginRequest requestDto)
		{
			if (ModelState.IsValid)
			{
				var existing_user = await _userManager.FindByEmailAsync(requestDto.Email);

				if (existing_user is null)
					return BadRequest(new AuthResult()
					{
						Result = false,
						Errors = new List<string>()
						{
							"Incorrect email"
						}
					});

				var isCorrect = await _userManager.CheckPasswordAsync(existing_user, requestDto.Password);

				if (!isCorrect)
					return BadRequest(new AuthResult()
					{
						Result = false,
						Errors = new List<string>()
						{
							"Incorrect password"
						}
					});

				var token = await GenerateJwtToken(existing_user);

				return Ok(token);

			}

			return BadRequest(ModelState);
		}

		[HttpPost]
		[Route("RefreshToken")]
		public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
		{
			if (ModelState.IsValid)
			{
				var result = VerifyAndGenerateToken(tokenRequest);

				if (result is null)
					return BadRequest(result);

				return Ok(result);
			}

			return BadRequest(ModelState);
		}

		private async Task<AuthResult> GenerateJwtToken(User user)
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

			var token = jwtTokenHandler.CreateToken(tokenDescriptor);
			var jwtToken = jwtTokenHandler.WriteToken(token);

			var refreshToken = new RefreshToken()
			{
				JwtId = token.Id,
				Token = RandomStringGeneration(64), //generate refresh token
				AddedDate = DateTime.UtcNow,
				ExpiryDate = DateTime.UtcNow.AddMonths(1),
				IsRevoked = false,
				IsUsed = false,
				UserId = user.Id.ToString()
			};

			await _refreshTokenService.CreateAsync(refreshToken);

			return new AuthResult()
			{
				Result = true,
				RefreshToken = refreshToken.Token,
				Token = jwtToken
			};
		}

		private string RandomStringGeneration(int lenght)
		{
			var randomNumber = new byte[lenght];

			using var generator = RandomNumberGenerator.Create();
			generator.GetBytes(randomNumber);

			return Convert.ToBase64String(randomNumber);
		}

		private async Task<AuthResult?> VerifyAndGenerateToken(TokenRequest tokenRequest)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();

			_tokenValidationParameters.ValidateLifetime = false; //test

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
				return new AuthResult() {
					Result = false,
					Errors = new List<string>()
					{
						"Expired token"
					}
				};

			var refreshTokens = await _refreshTokenService.GetAsync();
			var storeToken = refreshTokens!.FirstOrDefault(x => x.Token == tokenRequest.Token);

			if (storeToken is null)
				return new AuthResult()
				{
					Result = false,
					Errors = new List<string>()
					{
						"Invalid tokens"
					}
				};

			if (storeToken.IsUsed || storeToken.IsRevoked)
				return new AuthResult()
				{
					Result = false,
					Errors = new List<string>()
					{
						"Invalid tokens"
					}
				};

			var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)!.Value;
			if (storeToken.JwtId != jti || storeToken.ExpiryDate < DateTime.UtcNow)
				return new AuthResult()
				{
					Result = false,
					Errors = new List<string>()
					{
						"Invalid tokens"
					}
				};

			storeToken.IsUsed = true;
			await _refreshTokenService.UpdateAsync(storeToken.Id!, storeToken);

			var dbUser = await _userManager.FindByIdAsync(storeToken.UserId!);

			return await GenerateJwtToken(dbUser!);
		}

		private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
		{
			var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

			return dateTimeVal;
		}
	}
}
