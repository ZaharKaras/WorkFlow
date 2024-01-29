using Board.Core.Exceptions.Boards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.DeleteMember
{
	public class DeleteMemberHandler : IRequestHandler<DeleteMemberCommand>
	{
		private readonly IBoardRepository _boardRepository;

		public DeleteMemberHandler(IBoardRepository boardRepository)
		{
			_boardRepository = boardRepository;
		}

		public async Task Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
		{
			var board = await _boardRepository.GetByIdAsync(request.boardId);

			if (board == null)
				throw new BoardNotFoundException(request.boardId);

			board.RemoveMemberId(request.userId);

			await _boardRepository.UpdateAsync(board, cancellationToken);
		}
	}
}
