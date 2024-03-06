namespace RSS.Core.Exceptions
{
	public class FeedNotFoundException : Exception
	{
		public FeedNotFoundException(Guid boardId)
			: base($"The feed with the Id = {boardId.ToString()} was not found")
		{
		}
	}
}
