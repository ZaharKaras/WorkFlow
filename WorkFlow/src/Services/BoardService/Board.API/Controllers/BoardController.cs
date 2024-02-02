using Board.Application.Boards.Create;
using Board.Application.Boards.Delete;
using Board.Application.Boards.DTOs;
using Board.Application.Boards.Get;
using Board.Application.Boards.List;
using Board.Application.Boards.Update;
using Board.Application.BoardUsers.AddMember;
using Board.Application.BoardUsers.DeleteMember;
using Board.Application.BoardUsers.GetMembers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Board.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class BoardController : ControllerBase
	{
		private readonly IMediator _mediator;

		public BoardController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route("get-boards-by-user-id")]
		public async Task<ActionResult<IEnumerable<BoardsListDTO>>> GetBoardsByUserId(Guid userId)
		{
			var query = new ListBoardQuery(userId);

			var result = await _mediator.Send(query);

			if (result is null)
				return NotFound();

			return Ok(result);
		}

		[HttpGet]
		[Route("get-board-by-id")]
		public async Task<ActionResult<Core.Entities.Board>> GetBoardById(Guid id)
		{
			var query = new GetBoardQuery(id);

			var result = await _mediator.Send(query);

			return Ok(result);
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateBoard([FromBody] CreateBoardCommand command)
		{
			await _mediator.Send(command);

			return NoContent();
		}

		[HttpDelete]
		[Route("delete")]
		public async Task<IActionResult> DeleteBoard(Guid id)
		{
			var command = new DeleteBoardCommand(id);
			await _mediator.Send(command);

			return NoContent();
		}

		[HttpPut]
		[Route("update")]
		public async Task<IActionResult> UpdateBoard([FromBody] UpdateBoardCommand command)
		{
			await _mediator.Send(command);

			return NoContent();
		}

		[HttpGet]
		[Route("get-users-by-board-id")]
		public async Task<ActionResult<IEnumerable<Guid>>> GetUsersByBoardId([FromQuery] Guid boardId)
		{
			var query = new GetMembersQuery(boardId);

			var result = await _mediator.Send(query);

			if (result is null)
				return NotFound();

			return Ok(result);
		}

		[HttpPost]
		[Route("add-member")]
		public async Task<IActionResult> AddMemberToBoard([FromBody] AddMemberCommand command)
		{
			await _mediator.Send(command);

			return NoContent();
		}

		[HttpDelete]
		[Route("remove-member")]
		public async Task<IActionResult> RemoveMemberFromBoard([FromBody] DeleteMemberCommand command)
		{
			await _mediator.Send(command);

			return NoContent();
		}

	}
}
