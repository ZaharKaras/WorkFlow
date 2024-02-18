using AutoMapper;
using Board.Core.Exceptions.Boards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.BoardUsers.GetMembers
{
	public class GetMembersHandler : IRequestHandler<GetMembersQuery, IEnumerable<Guid>>
	{
		private readonly IBoardUserRepository _boardUserRepository;
		private readonly IMapper _mapper;

		public GetMembersHandler(IBoardUserRepository boardUserRepository, IMapper mapper)
		{
			_boardUserRepository = boardUserRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<Guid>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
		{
			var members = _mapper.Map<IEnumerable<Guid>>(
				await _boardUserRepository.GetByBoardIdAsync(request.boardId, cancellationToken));

			if (members is null)
			{
				throw new BoardNotFoundException(request.boardId);
			}

			return members;
		}
	}
}

