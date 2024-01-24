using Board.Core.Base;

namespace Board.Core.Entities
{
	public class Card : Entity
	{
		public string Title { get; set; } = null!;
		public string? Description { get; set; }
		public Guid BoardId { get; set; }
		public CardStatus Status { get; set; }
		public DateTime DeadLine { get; set; }
		public DateTime AddedDate { get; set; }

	}
}
