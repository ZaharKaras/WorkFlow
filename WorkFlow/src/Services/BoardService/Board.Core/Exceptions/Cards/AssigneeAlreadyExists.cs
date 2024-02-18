namespace Board.Core.Exceptions.Cards
{
	public class AssigneeAlreadyExists : CustomException
	{
		public AssigneeAlreadyExists(Guid id)
			: base($"Assignee with such id = {id.ToString()} has already been added.")
		{
			StatusCode = 409;
		}
	}
}
