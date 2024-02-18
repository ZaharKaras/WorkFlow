using Board.Core.Entities;

namespace Board.Core.Exceptions.Cards
{
	public class CardNotFoundException : CustomException
	{
		public CardNotFoundException(Guid cardId) 
			: base($"The card with the Id = {cardId.ToString()} was not found")
		{
			StatusCode = 404;
		}
	}
}
