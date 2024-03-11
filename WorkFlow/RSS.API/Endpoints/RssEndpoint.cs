using MediatR;
using Microsoft.AspNetCore.Mvc;
using RSS.Application.Feeds.Create;
using RSS.Application.Feeds.Delete;
using RSS.Application.Feeds.Get;
using RSS.Application.Feeds.List;


namespace RSS.API.Endpoints
{
	public static class RssEndpoints
	{
		public static void AddRssEndpoints(this IEndpointRouteBuilder app)
		{
			app.MapPost("api/feeds", AddFeedAsync);

			app.MapGet("api/users/{userId}/feeds", GetFeedsAsync).WithName(nameof(GetFeedsAsync));

			app.MapGet("api/feeds/{feedId}", GetFeedItemsAsync).WithName(nameof(GetFeedItemsAsync));

			app.MapDelete("api/feeds", DeleteFeedAsync);
		}

		public static async Task<IResult> AddFeedAsync(
			[FromBody] CreateFeedCommand command, 
			CancellationToken token,
			IMediator mediator)
		{
			await mediator.Send(command, token);

			return Results.NoContent();
		}

		public static async Task<IResult> GetFeedsAsync(
			[FromRoute] Guid userId, 
			CancellationToken token,
			IMediator mediator)
		{
			var query = new ListFeedQuery(userId);
			var result = await mediator.Send(query, token);

			return Results.Ok(result);
		}

		public static async Task<IResult> GetFeedItemsAsync(
			[FromRoute] Guid feedId,
			CancellationToken token,
			IMediator mediator)
		{
			var query = new GetFeedQuery(feedId);
			var result = await mediator.Send(query, token);

			return Results.Ok(result);
		}

		public static async Task<IResult> DeleteFeedAsync(
			[FromBody] Guid id, 
			CancellationToken token,
			IMediator mediator)
		{
			var command = new DeleteFeedCommand(id);
			await mediator.Send(command, token);

			return Results.NoContent();
		}


	}
}
