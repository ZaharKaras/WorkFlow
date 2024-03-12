using Identity.API.DTOs;
using Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IdentityController : ControllerBase
	{
		private readonly IIdentityService _identityService;

		public IdentityController(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequest registerRequest)
		{
			var result = await _identityService.RegisterAsync(registerRequest);

			return result.Match<IActionResult>(
				m => Ok(new TokenResponse() { Token = m.Token, RefreshToken = m.RefreshToken }),
				err => BadRequest(err));
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
		{
			var result = await _identityService.LoginAsync(loginRequest);

			return result.Match<IActionResult>(
				m => Ok(new TokenResponse() { Token = m.Token, RefreshToken = m.RefreshToken }),
				err => BadRequest(err));
		}

		[HttpPost]
		[Route("logout")]
		public async Task<IActionResult> Logout([FromBody] TokenRequest tokenRequest)
		{
			var result = await _identityService.LogoutAsync(tokenRequest);

			return result!.Match<IActionResult>(
				m => NoContent(),
				err => BadRequest(err));
		}

		[HttpPost]
		[Route("refresh-token")]
		public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
		{
			var result = await _identityService.RefreshTokenAsync(tokenRequest);

			return result!.Match<IActionResult>(
				m => Ok(new TokenResponse() { Token = m.Token, RefreshToken = m.RefreshToken }),
				err => BadRequest(err));
		}
	}
}
