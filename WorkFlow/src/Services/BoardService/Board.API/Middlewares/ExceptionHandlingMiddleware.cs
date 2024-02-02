using Board.Core.Exceptions.Boards;
using Board.Core.Exceptions.Cards;

namespace Board.API.Middlewares
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;

		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}

			catch (BoardNotFoundException ex)
			{
				_logger.LogError("Board not found");
				context.Response.StatusCode = StatusCodes.Status404NotFound;
				await context.Response.WriteAsync("Board not found");
			}
			catch (MemberAlreadyExists ex)
			{
				_logger.LogError("Member already exists");
				context.Response.StatusCode = StatusCodes.Status409Conflict;
				await context.Response.WriteAsync("Member already exists");
			}
			catch (CardNotFoundException ex)
			{
				_logger.LogError("Card not found");
				context.Response.StatusCode = StatusCodes.Status404NotFound;
				await context.Response.WriteAsync("Card not found");
			}
			catch (AssigneeAlreadyExists ex)
			{
				_logger.LogError("Assignee already exists");
				context.Response.StatusCode = StatusCodes.Status409Conflict;
				await context.Response.WriteAsync("Assignee already exists");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An unhandled exception occurred.");
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;
				await context.Response.WriteAsync("An unexpected error occurred");
			}
		}
	}
}
