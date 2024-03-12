using Identity.Core.Abstractions;

namespace Identity.Core.Errors
{
	public static class UserErrors
	{
		public static readonly Error UserNotFound = new Error(
			"Users.UserNotFound",
			"User does not exist");

		public static readonly Error PasswordChangeError = new Error(
			"Users.PasswordChangeError",
			"Error occurred while changing password");

		public static readonly Error UserAlreadyExists = new Error(
			"Users.UserAlreadyExists", 
			"User already exists");

		public static readonly Error UserCreationFailed = new Error(
			"Users.UserCreationFailed",
			"Failed to create user");

		public static readonly Error IncorrectPassword = new Error(
			"Auth.IncorrectPassword",
			"Incorrect password");
	}
}

