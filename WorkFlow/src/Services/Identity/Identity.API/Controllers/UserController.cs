using Identity.Infrastructure.DTOs;
using Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
			var result = await _userService.GetUserProfileAsync(this.User);

			return result.Match<IActionResult>(
				m => Ok(new UserResponse() { UserName = m.UserName, Email = m.Email}),
				err => BadRequest(err));
		}

		[HttpPut]
		[Route("edit")]
		public async Task<IActionResult> EditCurrentUserAsync([FromBody] UserResponse updatedUser)
		{
			var result = await _userService.UpdateUserProfileAsync(this.User, updatedUser);

			return result.Match<IActionResult>(
				m => NoContent(),
				err => BadRequest(err));
		}

		[HttpPut]
		[Route("password")]
		public async Task<IActionResult> ChangeCurrentPasswordAsync([FromBody] PasswordRequest password)
		{
			var result = await _userService.ChangePasswordAsync(this.User, password);

			return result.Match<IActionResult>(
				m => NoContent(),
				err => BadRequest(err));
		}
	}
}
