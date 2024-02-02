using Board.Application.Boards.DTOs;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.List
{
    public class ListBoardHandler : IRequestHandler<ListBoardQuery, IEnumerable<BoardsListDTO>>
	{
		private readonly IBoardRepository _boardRepository;

		public ListBoardHandler(IBoardRepository boardRepository)
		{
			_boardRepository = boardRepository;
		}

		public async Task<IEnumerable<BoardsListDTO>> Handle(ListBoardQuery request, CancellationToken cancellationToken)
		{
			var boards = await _boardRepository.GetByUserIdAsync(request.userId);

			var boardDTOs= boards.Select(board => new BoardsListDTO(
				board.Id,
				board.Name
			));

			return boardDTOs;
		}
	}
}
