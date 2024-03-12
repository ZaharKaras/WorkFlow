namespace Identity.API.DTOs
{
	public class RegistrationRequest
	{
		public string Email { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string ConfirmPassword { get; set; } = null!;

	}
}
