using System.ComponentModel.DataAnnotations;

namespace Identity.API.DTOs
{
	public class TokenRequest
	{
		[Required]
		public string Token { get; set; } = null!;
		[Required]
		public string RefreshToken { get; set; } = null!;
	}
}
