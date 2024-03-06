namespace RSS.Core.Exceptions
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException(Guid boardId)
			: base($"The user with the Id = {boardId.ToString()} was not found")
		{
		}
	}
}
