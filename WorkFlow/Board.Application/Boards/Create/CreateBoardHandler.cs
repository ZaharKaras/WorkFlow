using Board.Core.Entities;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.Create
{
	public class CreateBoardHandler : IRequestHandler<CreateBoardCommand>
	{
		private readonly IBoardRepository _boardRepository;

		public CreateBoardHandler(IBoardRepository boardRepository)
		{
			_boardRepository = boardRepository;
		}

		public async Task Handle(CreateBoardCommand request, CancellationToken cancellationToken)
		{
			var board = new Core.Entities.Board(
				Guid.NewGuid(),
				request.boardName,
				request.ownerId);

			await _boardRepository.AddAsync(board, cancellationToken);
		}
	}
}
