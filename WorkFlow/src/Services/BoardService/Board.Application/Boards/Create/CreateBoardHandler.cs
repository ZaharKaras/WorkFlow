using Board.Core.Entities;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.Create
{
	public class CreateBoardHandler : IRequestHandler<CreateBoardCommand>
	{
		private readonly IBoardRepository _boardRepository;
		private readonly IBoardUserRepository _boardUserRepository;

		public CreateBoardHandler(
			IBoardRepository boardRepository, 
			IBoardUserRepository boardUserRepository)
		{
			_boardRepository = boardRepository;
			_boardUserRepository = boardUserRepository;
		}

		public async Task Handle(CreateBoardCommand request, CancellationToken cancellationToken)
		{
			var board = new Core.Entities.Board(
				Guid.NewGuid(),
				request.boardName,
				request.ownerId);

			var boardUser = new BoardUser(
				Guid.NewGuid(),
				board.Id,
				board.OwnerId);

			await _boardRepository.AddAsync(board, cancellationToken);
			await _boardUserRepository.AddAsync(boardUser, cancellationToken);
		}
	}
}
