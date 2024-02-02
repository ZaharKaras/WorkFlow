namespace Board.Application.Boards.DTOs
{
    public record BoardDTO(Guid id, string name, Guid ownerId);
}
