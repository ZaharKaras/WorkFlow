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
		public List<Guid> MembersId { get; private set; } = new();

		public void Update(string name, Guid ownerId)
		{
			Name = name;
			OwnerId = ownerId;
		}

		public void AddMemberId(Guid userId)
		{
			MembersId.Add(userId);
		}

		public void RemoveMemberId(Guid userId)
		{
			MembersId.Remove(userId);
		}
	}
}
