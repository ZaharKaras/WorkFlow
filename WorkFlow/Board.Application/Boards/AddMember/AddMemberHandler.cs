using Board.Core.Exceptions.Boards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.AddMember
{
	public class AddMemberHandler : IRequestHandler<AddMemberCommand>
	{
		private readonly IBoardRepository _boardRepository;

		public AddMemberHandler(IBoardRepository boardRepository)
		{
			_boardRepository = boardRepository;
		}

		public async Task Handle(AddMemberCommand request, CancellationToken cancellationToken)
		{
			var board = await _boardRepository.GetByIdAsync(request.boardId);

			if (board == null)
				throw new BoardNotFoundException(request.boardId);

			if(board.MembersId.FirstOrDefault(request.userId) == request.userId)
				throw new MemberAlreadyExists(request.userId);

			board.AddMemberId(request.userId);

			await _boardRepository.UpdateAsync(board, cancellationToken);
		}
	}
}
