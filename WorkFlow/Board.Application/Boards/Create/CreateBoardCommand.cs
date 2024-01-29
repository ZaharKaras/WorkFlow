using MediatR;

namespace Board.Application.Boards.Create;
public record CreateBoardCommand(string boardName, Guid ownerId) : IRequest;
