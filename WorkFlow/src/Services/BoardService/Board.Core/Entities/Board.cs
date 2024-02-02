using Board.Core.Base;

namespace Board.Core.Entities
{
	public class Board : Entity
	{
		public Board(Guid id, string name, Guid ownerId)
		{
			Id = id;
			Name = name;
			OwnerId = ownerId;
		}

		public string Name { get; private set; } = string.Empty;
		public Guid OwnerId { get; private set; }
		public List<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();

		public void Update(string name, Guid ownerId)
		{
			Name = name;
			OwnerId = ownerId;
		}
	}
}
