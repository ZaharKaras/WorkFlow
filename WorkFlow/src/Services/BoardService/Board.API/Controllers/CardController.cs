using Board.Application.Cards.Create;
using Board.Application.Cards.Delete;
using Board.Application.Cards.DTOs;
using Board.Application.Cards.Update;
using Board.Application.CardUsers.AddAssignee;
using Board.Application.CardUsers.RemoveAssignee;
using Board.Application.Services.Interfaces;
using Board.Core.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Board.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CardController : ControllerBase
	{
		private readonly ICardService _cardService;

		public CardController(ICardService cardService)
		{
			_cardService = cardService;
		}

		[HttpGet]
		[Route("users/{userId}/cards")]
		public async Task<ActionResult<IEnumerable<CardListDTO>>> GetBoardsByUserId(
			Guid userId, CancellationToken token)
		{
			var result = await _cardService.GetCardsByUserId(userId, token);

			if (result is null)
				return NotFound();

			return Ok(result);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<Card>> GetCardById(
			Guid id, CancellationToken token)
		{
			var result = await _cardService.GetCardById(id, token);

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCard(
			[FromBody] CreateCardCommand command, CancellationToken token)
		{
			await _cardService.CreateCard(command, token);

			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCard(
			[FromBody] DeleteCardCommand command, CancellationToken token)
		{
			await _cardService.DeleteCard(command.cardId, token);

			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCard(
			[FromBody] UpdateCardCommand command, CancellationToken token)
		{
			await _cardService.UpdateCard(command, token);

			return NoContent();
		}

		[HttpGet]
		[Route("{cardId}/users")]
		public async Task<ActionResult<IEnumerable<Guid>>> GetUsersByCardId(
			[FromQuery] Guid cardId, CancellationToken token)
		{
			var result = await _cardService.GetAssigneesByCardId(cardId, token);

			if (result is null)
				return NotFound();

			return Ok(result);
		}


		[HttpPost]
		[Route("{cardId}/assignees")]
		public async Task<IActionResult> AddAssigneeToCard(
			[FromBody] AddAssigneeCommand command, CancellationToken token)
		{
			await _cardService.AddAssigneeToCard(command, token);

			return NoContent();
		}

		[HttpDelete]
		[Route("{cardId}/assignees")]
		public async Task<IActionResult> RemoveAssigneeFromCard(
			[FromBody] RemoveAssigneeCommand command, CancellationToken token)
		{
			await _cardService.RemoveAssigneeFromCard(command, token);

			return NoContent();
		}
	}
}
