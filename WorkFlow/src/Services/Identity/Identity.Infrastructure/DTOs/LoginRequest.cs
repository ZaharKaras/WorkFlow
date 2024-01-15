using System.ComponentModel.DataAnnotations;

namespace Identity.API.DTOs
{
	public class LoginRequest
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } = null!;
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
	}
}
