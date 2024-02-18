using Board.Application.Boards.Create;
using Board.Application.Boards.DTOs;
using Board.Application.Boards.Update;
using Board.Application.BoardUsers.AddMember;
using Board.Application.BoardUsers.DeleteMember;
using Board.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Board.API.Controllers
{
    [Route("api/[controller]s")]
	[ApiController]
	public class BoardController : ControllerBase
	{
		private readonly IBoardService _boardService;
		public BoardController(IBoardService boardService)
		{
			_boardService = boardService;
		}

		[HttpGet]
		[Route("users/{userId}/boards")]
		public async Task<ActionResult<IEnumerable<BoardsListDTO>>> GetBoardsByUserId(
			[FromBody] Guid userId, CancellationToken token)
		{
			var result = await _boardService.GetBoardsByUserId(userId, token);

			if (result is null)
				return NotFound();

			return Ok(result);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<Core.Entities.Board>> GetBoardById(
			[FromBody]Guid id, CancellationToken token)
		{
			var result = await _boardService.GetBoardById(id, token);

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateBoard(
			[FromBody] CreateBoardCommand command, CancellationToken token)
		{
			await _boardService.CreateBoard(command, token);

			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteBoard(
			[FromBody] Guid id, CancellationToken token)
		{
			await _boardService.DeleteBoard(id, token);

			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateBoard(
			[FromBody] UpdateBoardCommand command, CancellationToken token)
		{
			await _boardService.UpdateBoard(command, token);

			return NoContent();
		}

		[HttpGet]
		[Route("{boardId}/users")]
		public async Task<ActionResult<IEnumerable<Guid>>> GetUsersByBoardId(
			[FromBody] Guid boardId, CancellationToken token)
		{
			var result = await _boardService.GetMembersByBoardId(boardId, token);

			if (result is null)
				return NotFound();

			return Ok(result);
		}

		[HttpPost]
		[Route("{boardId}/members")]
		public async Task<IActionResult> AddMemberToBoard(
			[FromBody] AddMemberCommand command, CancellationToken token)
		{
			await _boardService.AddMemberToBoard(command, token);

			return NoContent();
		}

		[HttpDelete]
		[Route("{boardId}/members")]
		public async Task<IActionResult> RemoveMemberFromBoard(
			[FromBody] DeleteMemberCommand command, CancellationToken token)
		{
			await _boardService.RemoveMemberFromBoard(command, token);

			return NoContent();
		}
	}
}
