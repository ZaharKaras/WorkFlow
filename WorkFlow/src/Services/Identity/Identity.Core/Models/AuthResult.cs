namespace Identity.Core.Models
{
	public class AuthResult
	{
		public string? Token { get; set; }
		public string? RefreshToken { get; set; }
	}
}
