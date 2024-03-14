using Board.Application.Cards.Create;
using Board.Application.Cards.Delete;
using Board.Application.Cards.DTOs;
using Board.Application.Cards.Get;
using Board.Application.Cards.List;
using Board.Application.Cards.Update;
using Board.Application.CardUsers.AddAssignee;
using Board.Application.CardUsers.GetAssignees;
using Board.Application.CardUsers.RemoveAssignee;
using Board.Application.Services.Interfaces;
using Board.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Board.API.Controllers
{
    [Route("api/[controller]s")]
	[ApiController]
	public class CardController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CardController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route("users/{userId}/cards")]
		public async Task<ActionResult<IEnumerable<CardListDTO>>> GetCardsByUserId(
			[FromBody] ListCardQuery query, CancellationToken token)
		{
			var result = await _mediator.Send(query, token);

			return Ok(result);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<Card>> GetCardById(
			[FromBody] GetCardQuery query, CancellationToken token)
		{
			var result = await _mediator.Send(query, token);

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCard(
			[FromBody] CreateCardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCard(
			[FromBody] DeleteCardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCard(
			[FromBody] UpdateCardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpGet]
		[Route("{cardId}/users")]
		public async Task<ActionResult<IEnumerable<Guid>>> GetUsersByCardId(
			[FromBody] GetAssigneesQuery query, CancellationToken token)
		{
			var result = await _mediator.Send(query, token);

			return Ok(result);
		}


		[HttpPost]
		[Route("{cardId}/assignees")]
		public async Task<IActionResult> AddAssigneeToCard(
			[FromBody] AddAssigneeCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpDelete]
		[Route("{cardId}/assignees")]
		public async Task<IActionResult> RemoveAssigneeFromCard(
			[FromBody] RemoveAssigneeCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}
	}
}
