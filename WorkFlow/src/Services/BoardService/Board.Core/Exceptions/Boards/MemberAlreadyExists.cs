namespace Board.Core.Exceptions.Boards
{
	public class MemberAlreadyExists : CustomException
	{
		public MemberAlreadyExists(Guid memberId)
			: base($"Member with such id = {memberId.ToString()} has already been added.")
		{
			StatusCode = 409;
		}
	}
}
