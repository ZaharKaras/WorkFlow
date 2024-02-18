using Board.Core.Exceptions;
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

			catch (CustomException ex)
			{
				_logger.LogError(ex.Message);
				context.Response.StatusCode = ex.StatusCode;
				await context.Response.WriteAsync(ex.Message);
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
