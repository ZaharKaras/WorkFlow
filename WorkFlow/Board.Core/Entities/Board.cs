using Board.Core.Base;

namespace Board.Core.Entities
{
	public class Board : Entity
	{
		public string Name { get; set; } = null!;
		public Guid UserId { get; set; }
		public ICollection<Card>? Cards { get; set; }
	}
}
