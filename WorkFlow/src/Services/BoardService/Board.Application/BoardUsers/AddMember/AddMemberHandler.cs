using Board.Core.Entities;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.BoardUsers.AddMember
{
	public class AddMemberHandler : IRequestHandler<AddMemberCommand>
	{
		private readonly IBoardUserRepository _boardUserRepository;

		public AddMemberHandler(IBoardUserRepository boardUserRepository)
		{
			_boardUserRepository = boardUserRepository;
		}

		public async Task Handle(AddMemberCommand request, CancellationToken cancellationToken)
		{
			var boardUser = new BoardUser(
				Guid.NewGuid(),
				request.boardId,
				request.userId);

			await _boardUserRepository.AddAsync(boardUser, cancellationToken);
		}
	}
}
