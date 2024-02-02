using Board.Core.Base;

namespace Board.Core.Entities
{
	public class Card : Entity
	{
		public Card(Guid id, string title, Guid boardId)
		{
			Id = id;
			Title = title;
			BoardId = boardId;
			AddedDate = DateTime.UtcNow;
		}
		public string Title { get; private set; } = null!;
		public string? Description { get; private set; }
		public Guid BoardId { get; private set; }
		public CardStatus Status { get; private set; } = CardStatus.ToDo;
		public DateTime DeadLine { get; private set; }
		public DateTime AddedDate { get; private set; }
		public List<CardUser> CardUsers { get; set; } = new List<CardUser>();

		public void SetDeadLine(DateTime deadLine)
		{
			DeadLine = deadLine;
		}

		public void UpdateTitle(string title)
		{
			Title = title;
		}

		public void SetDescription(string? description)
		{
			Description = description;
		}

		public void ChangeStatus(CardStatus status)
		{
			Status = status;
		}

	}
}
