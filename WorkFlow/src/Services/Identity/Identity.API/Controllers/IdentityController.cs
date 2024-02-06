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
			return Ok(await _identityService.Register(registerRequest));
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
		{
			return Ok(await _identityService.Login(loginRequest));
		}

		[HttpPost]
		[Route("logout")]
		public async Task<IActionResult> Logout([FromBody] TokenRequest tokenRequest)
		{
			return Ok(await _identityService.Logout(tokenRequest));
		}

		[HttpPost]
		[Route("refresh-token")]
		public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
		{
			return Ok(await _identityService.RefreshToken(tokenRequest));
		}


	}
}
