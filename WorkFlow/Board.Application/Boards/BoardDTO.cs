namespace Board.Application.Boards
{
	public record BoardDTO(Guid id, string name, Guid ownerId, List<Guid> membersId);
}
