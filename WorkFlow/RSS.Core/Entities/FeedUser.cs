namespace RSS.Core.Entities
{
	public class FeedUser
	{
		public FeedUser(Guid userId, Guid feedId)
		{
			UserId = userId;
			FeedId = feedId;
		}

		public Guid UserId { get; set; }
		public Guid FeedId { get; set; }
		public Feed Feed { get; set; } = null!;
	}
}
