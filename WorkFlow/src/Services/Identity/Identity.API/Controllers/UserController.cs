using Identity.Infrastructure.DTOs;
using Identity.Infrastructure.Services;
using Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		[Route("profile")]
		public async Task<IActionResult> GetCurrentUserProfileAsync()
		{
			return Ok(await _userService.GetUserProfileAsync(this.User));
		}

		[HttpPut]
		[Route("edit")]
		public async Task<IActionResult> EditCurrentUserAsync(UserResponse updatedUser)
		{
			await _userService.UpdateUserProfileAsync(this.User, updatedUser);

			return NoContent();
		}

		[HttpPut]
		[Route("password")]
		public async Task<IActionResult> ChangeCurrentPasswordAsync(PasswordRequest password)
		{
			await _userService.ChangePasswordAsync(this.User, password);

			return NoContent();
		}
	}
}
