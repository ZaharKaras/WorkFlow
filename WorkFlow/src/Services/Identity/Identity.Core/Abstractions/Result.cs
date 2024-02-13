namespace Identity.Core.Abstractions
{
	public class Result<TValue, TError>
	{
		public readonly TValue? _value;
		public readonly TError? _error;

		private Result(TValue value)
		{
			IsError = false;
			_value = value;
			_error = default;
		}

		private Result(TError error)
		{
			IsError = true;
			_value = default;
			_error = error;
		}

		public bool IsError { get; }
		public bool isSuccess => !IsError;

		public static implicit operator Result<TValue, TError>(TValue value) 
			=> new Result<TValue, TError>(value);

		public static implicit operator Result<TValue, TError>(TError error) 
			=> new Result<TValue, TError>(error);

		public TResult Match<TResult>(
			Func<TValue, TResult> success,
			Func<TError, TResult> failure) =>
			!IsError ? success(_value!) : failure(_error!);
	}
}
