using AutoMapper;
using Board.Application.Boards.Create;
using Board.Application.Boards.DTOs;
using Board.Core.Entities;

namespace Board.Application.Mapper
{
	public class BoardProfile : Profile
	{
		BoardProfile()
		{
			CreateMap<CreateBoardCommand, Core.Entities.Board>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

			CreateMap<Core.Entities.Board, BoardUser>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
				.ForMember(dest => dest.BoardId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.OwnerId));


			CreateMap<Core.Entities.Board, BoardDTO>();

			CreateMap<Core.Entities.Board, BoardsListDTO>();
		}
	}
}
