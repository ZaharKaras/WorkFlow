using Identity.Core.Abstractions;

namespace Identity.Core.Errors
{
	public static class TokenErrors
	{
		public static readonly Error RefreshTokenNotFound = new Error(
			"RefreshTokens.RefreshTokenNotFound", 
			"Refresh token doesn't exist");

		public static readonly Error ExpiredToken = new Error(
			"Tokens.ExpiredToken", "Expired token");

		public static readonly Error InvalidTokens = new Error(
			"Tokens.InvalidTokens", "Invalid tokens");

		public static readonly Error TokenNotFound = new Error(
			"RefreshTokens.TokenNotFound",
			"Token does not exist");
	}
}
