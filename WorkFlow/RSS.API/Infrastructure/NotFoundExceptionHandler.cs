using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RSS.Core.Exceptions;
using Serilog;

namespace RSS.API.Infrastructure
{
	public class NotFoundExceptionHandler : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(
			HttpContext httpContext,
			Exception exception, 
			CancellationToken cancellationToken)
		{
			if (exception is not UserNotFoundException &&
				exception is not FeedNotFoundException)
			{
				return false;
			}

			Log.Error(exception, "Exception occurred: {Message}", exception.Message);

			var problemDetails = new ProblemDetails
			{
				Status = StatusCodes.Status404NotFound,
				Title = "Not found"
			};

			httpContext.Response.StatusCode = problemDetails.Status.Value;

			await httpContext.Response
				.WriteAsJsonAsync(problemDetails, cancellationToken);

			return true;
		}
	}
}
