namespace Board.Application.Cards.DTOs
{
    public record CardDTO(
        Guid id,
        string title,
        string? description,
        Guid boardId,
        CardStatus status,
        DateTime deadLine,
        DateTime addedDate);
}
