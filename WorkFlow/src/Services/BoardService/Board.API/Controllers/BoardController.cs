using Board.API.Email.Interfaces;
using Board.Application.Boards.Create;
using Board.Application.Boards.Delete;
using Board.Application.Boards.DTOs;
using Board.Application.Boards.Get;
using Board.Application.Boards.List;
using Board.Application.Boards.Update;
using Board.Application.BoardUsers.AddMember;
using Board.Application.BoardUsers.DeleteMember;
using Board.Application.BoardUsers.GetMembers;
using Board.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Board.API.Controllers
{
    [Route("api/[controller]s")]
	[ApiController]
	public class BoardController : ControllerBase
	{
		private readonly IMediator _mediator;
		public BoardController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route("users/{userId}/boards")]
		public async Task<ActionResult<IEnumerable<BoardsListDTO>>> GetBoardsByUserId(
			[FromBody] Guid userId, CancellationToken token)
		{
			var query = new ListBoardQuery(userId);
			var result = await _mediator.Send(query, token);

			return Ok(result);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<Core.Entities.Board>> GetBoardById(
			[FromBody]Guid id, CancellationToken token)
		{
			var query = new GetBoardQuery(id);
			var result = await _mediator.Send(query, token);

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateBoard(
			[FromBody] CreateBoardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteBoard(
			[FromBody] DeleteBoardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateBoard(
			[FromBody] UpdateBoardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpGet]
		[Route("{boardId}/users")]
		public async Task<ActionResult<IEnumerable<Guid>>> GetUsersByBoardId(
			[FromBody] GetMembersQuery query, CancellationToken token)
		{
			var result = await _mediator.Send(query, token);

			return Ok(result);
		}

		[HttpPost]
		[Route("{boardId}/members")]
		public async Task<IActionResult> AddMemberToBoard(
			[FromBody] AddMemberCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}

		[HttpDelete]
		[Route("{boardId}/members")]
		public async Task<IActionResult> RemoveMemberFromBoard(
			[FromBody] DeleteMemberCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);

			return NoContent();
		}
	}
}
