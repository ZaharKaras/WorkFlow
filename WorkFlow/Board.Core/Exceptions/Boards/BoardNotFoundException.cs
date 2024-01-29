namespace Board.Core.Exceptions.Boards
{
	public class BoardNotFoundException : Exception
	{
		public BoardNotFoundException(Guid boardId)
			: base($"The board with the Id = {boardId.ToString()} was not found")
		{
		}
	}
}
