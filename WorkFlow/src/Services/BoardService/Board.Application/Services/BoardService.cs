using Board.Application.Boards.Create;
using Board.Application.Boards.Delete;
using Board.Application.Boards.DTOs;
using Board.Application.Boards.Get;
using Board.Application.Boards.List;
using Board.Application.Boards.Update;
using Board.Application.BoardUsers.AddMember;
using Board.Application.BoardUsers.DeleteMember;
using Board.Application.BoardUsers.GetMembers;
using Board.Application.Services.Interfaces;
using MediatR;

namespace Board.Application.Services
{
	public class BoardService : IBoardService
	{
		private readonly IMediator _mediator;

		public BoardService(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IEnumerable<BoardsListDTO>> GetBoardsByUserId(Guid userId, CancellationToken token)
		{
			var query = new ListBoardQuery(userId);
			var result = await _mediator.Send(query, token);

			return result;
		}

		public async Task<BoardDTO> GetBoardById(Guid id, CancellationToken token)
		{
			var query = new GetBoardQuery(id);
			var result = await _mediator.Send(query, token);

			return result;
		}

		public async Task CreateBoard(CreateBoardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);
		}

		public async Task DeleteBoard(Guid id, CancellationToken token)
		{
			var command = new DeleteBoardCommand(id);
			await _mediator.Send(command, token);
		}

		public async Task UpdateBoard(UpdateBoardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);
		}

		public async Task<IEnumerable<Guid>> GetMembersByBoardId(Guid boardId, CancellationToken token)
		{
			var query = new GetMembersQuery(boardId);
			var result = await _mediator.Send(query, token);

			return result;
		}

		public async Task AddMemberToBoard(AddMemberCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);
		}

		public async Task RemoveMemberFromBoard(DeleteMemberCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);
		}
	}
}
