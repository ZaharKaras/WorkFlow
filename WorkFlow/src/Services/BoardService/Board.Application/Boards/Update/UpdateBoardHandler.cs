﻿using Board.Core.Exceptions.Boards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.Update
{
	public class UpdateBoardHandler : IRequestHandler<UpdateBoardCommand>
	{
		private readonly IBoardRepository _boardRepository;

		public UpdateBoardHandler(IBoardRepository boardRepository)
		{
			_boardRepository = boardRepository;
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
		}
	}
}
