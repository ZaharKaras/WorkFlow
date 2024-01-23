using System.ComponentModel.DataAnnotations;

namespace Identity.API.DTOs
{
	public class RegistrationRequest
	{
		[Required, EmailAddress]
		public string Email { get; set; } = null!;
		[Required, MaxLength(20)]
		public string Name { get; set; } = null!;
		[Required, MaxLength(20)]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
		[Required, Compare(nameof(Password), ErrorMessage = "Password do not math")]
		public string ComfirmPassword { get; set; } = null!;

	}
}
