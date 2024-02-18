using Board.Core.Base;

namespace Board.Core.Entities
{
	public class BoardUser : Entity
	{
		public BoardUser(Guid id, Guid boardId, Guid userId)
		{
			Id = id;
			BoardId = boardId;
			UserId = userId;
		}
		public Guid UserId { get; set; }
		public Guid BoardId { get; set; }
		public Board Board { get; set; } = null!;
	}
}
