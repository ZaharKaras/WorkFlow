namespace RSS.Core.Abstractions
{
	public class Entity
	{
		protected Entity(Guid id) => Id = Id;
		protected Entity() { }
		public Guid Id { get; set; }
	}
}
