namespace Board.Core.Exceptions
{
	public class CustomException : Exception
	{
		public int StatusCode { get; set; }
		public CustomException(string message)
			: base(message)
		{
		}
	}
}
