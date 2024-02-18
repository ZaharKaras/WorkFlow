using AutoMapper;
using Board.Application.Boards.DTOs;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.List
{
    public class ListBoardHandler : IRequestHandler<ListBoardQuery, IEnumerable<BoardsListDTO>>
	{
		private readonly IBoardRepository _boardRepository;
		private readonly IMapper _mapper;

		public ListBoardHandler(IBoardRepository boardRepository, IMapper mapper)
		{
			_boardRepository = boardRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<BoardsListDTO>> Handle(ListBoardQuery request, CancellationToken cancellationToken)
		{
			var boards = await _boardRepository.GetByUserIdAsync(request.userId);

			var boardDTOs = _mapper.Map<List<BoardsListDTO>>(boards);

			return boardDTOs;
		}
	}
}
