
using Board.Application.BoardUsers.GetMembers;
using Board.Application.Cards.Create;
using Board.Application.Cards.Delete;
using Board.Application.Cards.DTOs;
using Board.Application.Cards.List;
using Board.Application.Cards.Update;
using Board.Application.CardUsers.AddAssignee;
using Board.Application.CardUsers.GetAssignees;
using Board.Application.CardUsers.RemoveAssignee;
using Board.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Board.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CardController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CardController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route("get-card-by-user-id")]
		public async Task<ActionResult<IEnumerable<CardListDTO>>> GetBoardsByUserId(
			Guid userId, CancellationToken token)
		{
			var query = new ListCardQuery(userId);

			var result = await _mediator.Send(query, token);

			if (result is null)
				return NotFound();

			return Ok(result);
		}

		[HttpGet]
		[Route("get-card-by-id")]
		public async Task<ActionResult<Card>> GetCardById(
			Guid id, CancellationToken token)
		{
			var query = new ListCardQuery(id);

			var result = await _mediator.Send(query, token);

			return Ok(result);
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateCard(
			[FromBody] CreateCardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpDelete]
		[Route("delete")]
		public async Task<IActionResult> DeleteCard(
			[FromBody] DeleteCardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpPut]
		[Route("update")]
		public async Task<IActionResult> UpdateCard(
			[FromBody] UpdateCardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpGet]
		[Route("get-users-by-card-id")]
		public async Task<ActionResult<IEnumerable<Guid>>> GetUsersByCardId(
			[FromQuery] Guid boardId, CancellationToken token)
		{
			var query = new GetAssigneesQuery(boardId);

			var result = await _mediator.Send(query, token);

			if (result is null)
				return NotFound();

			return Ok(result);
		}


		[HttpPost]
		[Route("add-assignee")]
		public async Task<IActionResult> AddAssigneeToCard(
			[FromBody] AddAssigneeCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpDelete]
		[Route("remove-assignee")]
		public async Task<IActionResult> RemoveAssigneeFromCard(
			[FromBody] RemoveAssigneeCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}
	}
}
