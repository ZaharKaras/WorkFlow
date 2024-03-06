using MediatR;
using Microsoft.AspNetCore.Mvc;
using RSS.Application.DTOs;
using RSS.Application.Feeds.Create;
using RSS.Application.Feeds.Delete;
using RSS.Application.Feeds.Get;
using RSS.Application.Feeds.List;


namespace RSS.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RssController : ControllerBase
	{
		private readonly IMediator _mediator;

		public RssController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[Route("feeds")]
		public async Task<IActionResult> AddFeedAsync(
			[FromBody] CreateFeedCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpGet]
		[Route("users/{userId}")]
		public async Task<ActionResult<IEnumerable<FeedDTO>>> GetFeedsAsync(
			[FromRoute] Guid userId, CancellationToken token)
		{
			var query = new ListFeedQuery(userId);
			var result = await _mediator.Send(query, token);

			return Ok(result);
		}

		[HttpGet]
		[Route("feeds/{feedId}")]
		public async Task<ActionResult<IEnumerable<FeedDTO>>> GetFeedItemsAsync(
			[FromRoute] Guid feedId, CancellationToken token)
		{
			var query = new GetFeedQuery(feedId);
			var result = await _mediator.Send(query, token);

			return Ok(result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteFeedAsync(
			[FromBody] Guid id, CancellationToken token)
		{
			var command = new DeleteFeedCommand(id);
			await _mediator.Send(command, token);

			return NoContent();
		}


	}
}
