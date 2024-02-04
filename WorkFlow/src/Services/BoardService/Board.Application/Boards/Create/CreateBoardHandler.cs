using AutoMapper;
using Board.Core.Entities;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Boards.Create
{
	public class CreateBoardHandler : IRequestHandler<CreateBoardCommand>
	{
		private readonly IBoardRepository _boardRepository;
		private readonly IBoardUserRepository _boardUserRepository;
		private readonly IMapper _mapper;

		public CreateBoardHandler(
			IBoardRepository boardRepository, 
			IBoardUserRepository boardUserRepository,
			IMapper mapper)
		{
			_boardRepository = boardRepository;
			_boardUserRepository = boardUserRepository;
			_mapper = mapper;
		}

		public async Task Handle(CreateBoardCommand request, CancellationToken cancellationToken)
		{
			var board = _mapper.Map<Core.Entities.Board>(request);

			var boardUser = _mapper.Map<BoardUser>(request);

			await _boardRepository.AddAsync(board, cancellationToken);
			await _boardUserRepository.AddAsync(boardUser, cancellationToken);
		}
	}
}
