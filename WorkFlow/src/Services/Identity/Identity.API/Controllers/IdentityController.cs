using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IdentityController : ControllerBase
	{
		private readonly string TokenSecret;
		private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8);

		[HttpPost("token")]


	}
}
