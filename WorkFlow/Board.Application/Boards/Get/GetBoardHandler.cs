using Board.Core.Exceptions.Boards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.Get
{
	public class GetBoardHandler : IRequestHandler<GetBoardQuery, BoardDTO>
	{
		private readonly IBoardRepository _boardRepository;

		public GetBoardHandler(IBoardRepository boardRepository)
		{
			_boardRepository = boardRepository;
		}

		public async Task<BoardDTO> Handle(GetBoardQuery request, CancellationToken cancellationToken)
		{
			var board = await _boardRepository.GetByIdAsync(request.boardId);

			if (board == null)
				throw new BoardNotFoundException(request.boardId);

			return new BoardDTO(
				board.Id,
				board.Name,
				board.OwnerId,
				board.MembersId
			);
		}
	}
}
