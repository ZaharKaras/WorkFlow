using Board.Core.Exceptions.Boards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.Delete
{
	public class DeleteBoardHandler : IRequestHandler<DeleteBoardCommand>
	{
		private readonly IBoardRepository _boardRepository;

		public DeleteBoardHandler(IBoardRepository boardRepository)
		{
			_boardRepository = boardRepository;
		}

		public async Task Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
		{
			var board = await _boardRepository.GetByIdAsync(request.boardId);

			if (board == null)
				throw new BoardNotFoundException(request.boardId);

			await _boardRepository.DeleteAsync(request.boardId, cancellationToken);
			await _boardRepository.SaveChangesAsync(cancellationToken);
		}
	}
}
