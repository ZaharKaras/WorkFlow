namespace Board.Application.Cards
{
	public record CardDTO(
		Guid id, 
		string title, 
		string? description, 
		Guid boardId, 
		List<string> assigneesId, 
		CardStatus status,
		DateTime deadLine,
		DateTime addedDate);
}
