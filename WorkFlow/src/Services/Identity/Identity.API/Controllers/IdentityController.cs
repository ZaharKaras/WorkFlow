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
			var result = await _identityService.Register(registerRequest);
			return Ok(result);
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
		{
			var result = await _identityService.Login(loginRequest);
			return Ok(result);
		}

		[HttpPost]
		[Route("logout")]
		public async Task<IActionResult> Logout([FromBody] TokenRequest tokenRequest)
		{
			var result = await _identityService.Logout(tokenRequest);
			return Ok(result);
		}

		[HttpPost]
		[Route("refresh-token")]
		public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
		{
			var result = await _identityService.RefreshToken(tokenRequest);
			return Ok(result);
		}


	}
}
