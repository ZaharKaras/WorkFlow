using RSS.Core.Abstractions;

namespace RSS.Core.Entities
{
	public class Feed : Entity
	{
		public string Link { get; set; } = null!;
		public string Title { get; set; } = null!;
		public List<FeedUser> Users { get; set; } = new();
	}
}
