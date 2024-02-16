using RSS.Core.Abstractions;

namespace RSS.Core.Entities
{
	public class Feed : Entity
	{
		public string Uri { get; set; } = null!;
		public string Title { get; set; } = null!;
		public string Category { get; set; } = null!;
		public string Description { get; set; } = null!;
		public Guid UserId { get; set; }
		public DateTime Added { get; set; }
		public DateTime LastChecked { get; set; }
		public virtual ICollection<Item> Items { get; set; } = new List<Item>();
	}
}
