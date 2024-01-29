using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.List
{
	public class ListBoardHandler : IRequestHandler<ListBoardQuery, BoardsListDTO>
	{
		private readonly IBoardRepository _boardRepository;

		public ListBoardHandler(IBoardRepository boardRepository)
		{
			_boardRepository = boardRepository;
		}

		public async Task<BoardsListDTO> Handle(ListBoardQuery request, CancellationToken cancellationToken)
		{
			var boards = await _boardRepository.GetByUserIdAsync(request.userId);

			return new BoardsListDTO(boards.Select(board => board.Name).ToList());
		}
	}
}
