namespace RSS.Core.Entities
{
	public class Item
	{
		public Item() { }

		public string Title { get; set; } = null!;
		public string Uri { get; set; } = null!;
		public DateTime PubDate { get; set; }
	}
}
