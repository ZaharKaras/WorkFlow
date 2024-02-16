using RSS.Core.Abstractions;

namespace RSS.Core.Entities
{
	public class Item : Entity
	{
		public Feed Feed { get; set; } = null!;
		public string Title { get; set; } = null!;
		public string Uri { get; set; } = null!;
		public DateTime Published { get; set; }
		public DateTime Read { get; set; }
	}
}
