using Board.Core.Entities;

namespace Board.Core.Exceptions.Cards
{
	public class CardNotFoundException : Exception
	{
		public CardNotFoundException(Guid cardId) 
			: base($"The card with the Id = {cardId.ToString()} was not found")
		{ }
	}
}
