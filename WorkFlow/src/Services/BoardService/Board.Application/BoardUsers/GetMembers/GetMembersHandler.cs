using Board.Core.Exceptions.Boards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.BoardUsers.GetMembers
{
	public class GetMembersHandler : IRequestHandler<GetMembersQuery, IEnumerable<Guid>>
	{
		private readonly IBoardUserRepository _boardUserRepository;

		public GetMembersHandler(IBoardUserRepository boardUserRepository)
		{
			_boardUserRepository = boardUserRepository;
		}

		public async Task<IEnumerable<Guid>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
		{
			var members = await _boardUserRepository.GetByBoardIdAsync(request.boardId, cancellationToken);

			if (members is null)
			{
				throw new BoardNotFoundException(request.boardId);
			}

			return members;
		}
	}
}

