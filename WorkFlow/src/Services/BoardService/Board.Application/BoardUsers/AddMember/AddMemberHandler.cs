using AutoMapper;
using Board.Core.Entities;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.BoardUsers.AddMember
{
	public class AddMemberHandler : IRequestHandler<AddMemberCommand>
	{
		private readonly IBoardUserRepository _boardUserRepository;
		private readonly IMapper _mapper;

		public AddMemberHandler(IBoardUserRepository boardUserRepository,
			IMapper mapper)
		{
			_boardUserRepository = boardUserRepository;
			_mapper = mapper;
		}

		public async Task Handle(AddMemberCommand request, CancellationToken cancellationToken)
		{
			var boardUser = _mapper.Map<BoardUser>(request);

			await _boardUserRepository.AddAsync(boardUser, cancellationToken);
			await _boardUserRepository.SaveChangesAsync(cancellationToken);
		}
	}
}
