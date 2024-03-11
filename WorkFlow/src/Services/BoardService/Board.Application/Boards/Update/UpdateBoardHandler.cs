using Board.Core.Exceptions.Boards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.Update
{
	public class UpdateBoardHandler : IRequestHandler<UpdateBoardCommand>
	{
		private readonly IBoardRepository _boardRepository;
		private readonly IBoardUserRepository _userRepository;
		private readonly IEmailService _emailService;

		public UpdateBoardHandler(
			IBoardRepository boardRepository, 
			IBoardUserRepository userRepository, 
			IEmailService emailService)
		{
			_boardRepository = boardRepository;
			_userRepository = userRepository;
			_emailService = emailService;
		}

		public async Task Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
		{
			var board = await _boardRepository.GetByIdAsync(request.id);

			if (board is null)
			{
				throw new BoardNotFoundException(request.id);
			}

			board.Update(request.boardName, request.ownerId);

			_boardRepository.Update(board);

			await _boardRepository.SaveChangesAsync(cancellationToken);
			await NotificateMembers(request.id);
		}

		public async Task NotificateMembers(Guid boardId)
		{
			var members = await _userRepository.GetByBoardIdAsync(boardId);

			foreach(var member in members)
			{
				_emailService.SendEmail(member.ToString(), $"Board: {boardId}", "Board's state was changed");
			}
		}
	}
}
