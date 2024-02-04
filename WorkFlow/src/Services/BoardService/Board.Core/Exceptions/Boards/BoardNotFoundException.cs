using System.Diagnostics;
using System.Net;

namespace Board.Core.Exceptions.Boards
{
	public class BoardNotFoundException : CustomException
	{
		public BoardNotFoundException(Guid boardId)
			: base($"The board with the Id = {boardId.ToString()} was not found")
		{
			StatusCode = 404;
		}
	}
}
