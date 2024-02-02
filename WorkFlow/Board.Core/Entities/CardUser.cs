using Board.Core.Base;

namespace Board.Core.Entities
{
	public class CardUser : Entity
	{
		public CardUser(Guid id ,Guid cardId, Guid userId)
		{
			Id = id;
			CardId = cardId;
			UserId = userId;
		}

		public Guid CardId { get; set; }
		public Guid UserId { get; set; }
		public Card Card { get; set; } = null!;
	}
}
