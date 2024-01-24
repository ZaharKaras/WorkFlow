namespace Board.Core.Base
{
	public class Entity
	{
		protected Entity(Guid id) => Id = Id;
		protected Entity() { }
		public Guid Id { get; set; }
	}
}
