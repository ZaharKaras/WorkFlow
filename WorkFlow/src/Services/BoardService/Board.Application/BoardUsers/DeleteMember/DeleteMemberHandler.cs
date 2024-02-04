using Board.Core.Exceptions.Boards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.BoardUsers.DeleteMember
{
	public class DeleteMemberHandler : IRequestHandler<DeleteMemberCommand>
	{
		private readonly IBoardUserRepository _boardUserRepository;

		public DeleteMemberHandler(IBoardUserRepository boardUserRepository)
		{
			_boardUserRepository = boardUserRepository;
		}

		public async Task Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
		{
			var boardUser = await _boardUserRepository.GetByIdAsync(request.boardId);

			if (boardUser == null)
				throw new BoardNotFoundException(request.boardId);

			await _boardUserRepository.DeleteAsync(boardUser.Id, cancellationToken);
			await _boardUserRepository.SaveChangesAsync(cancellationToken);
		}
	}
}
