namespace Board.Core.Exceptions.Cards
{
	public class AssigneeAlreadyExists : Exception
	{
		public AssigneeAlreadyExists(Guid id) 
			: base($"Assignee with such id = {id.ToString()} has already been added.") 
		{ }
	}
}
