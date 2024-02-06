namespace Identity.API.Middlewares
{
	public class CustomExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<CustomExceptionMiddleware> _logger;

		public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = 500;

				var error = new Error
				{
					StatusCode = context.Response.StatusCode.ToString(),
					Message = ex.Message,
				};

				await context.Response.WriteAsync(error.ToString());

				_logger.LogError($"Error handled by middleware => { ex.Message.ToString()}");
			}
		}
	}
}
