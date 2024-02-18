using AutoMapper;
using Board.Application.Boards.DTOs;
using Board.Core.Exceptions.Boards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.Get
{
    public class GetBoardHandler : IRequestHandler<GetBoardQuery, BoardDTO>
	{
		private readonly IBoardRepository _boardRepository;
		private readonly IMapper _mapper;

		public GetBoardHandler(IBoardRepository boardRepository, IMapper mapper)
		{
			_boardRepository = boardRepository;
			_mapper = mapper;
		}

		public async Task<BoardDTO> Handle(GetBoardQuery request, CancellationToken cancellationToken)
		{
			var board = await _boardRepository.GetByIdAsync(request.boardId);

			if (board is null)
			{
				throw new BoardNotFoundException(request.boardId);
			}

			return _mapper.Map<BoardDTO>(board);
		}
	}
}
