using Board.Application.Boards.Create;
using Board.Application.Boards.DTOs;
using Board.Application.Boards.Update;
using Board.Application.BoardUsers.AddMember;
using Board.Application.BoardUsers.DeleteMember;

namespace Board.Application.Services.Interfaces
{
	public interface IBoardService
	{
		Task<IEnumerable<BoardsListDTO>> GetBoardsByUserId(Guid userId, CancellationToken token);
		Task<BoardDTO> GetBoardById(Guid id, CancellationToken token);
		Task CreateBoard(CreateBoardCommand command, CancellationToken token);
		Task DeleteBoard(Guid id, CancellationToken token);
		Task UpdateBoard(UpdateBoardCommand command, CancellationToken token);
		Task<IEnumerable<Guid>> GetMembersByBoardId(Guid boardId, CancellationToken token);
		Task AddMemberToBoard(AddMemberCommand command, CancellationToken token);
		Task RemoveMemberFromBoard(DeleteMemberCommand command, CancellationToken token);
	}
}
