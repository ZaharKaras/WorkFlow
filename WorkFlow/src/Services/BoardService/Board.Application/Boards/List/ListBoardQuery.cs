using Board.Application.Boards.DTOs;
using MediatR;

namespace Board.Application.Boards.List
{
    public record ListBoardQuery(Guid userId) : IRequest<IEnumerable<BoardsListDTO>>;
}
