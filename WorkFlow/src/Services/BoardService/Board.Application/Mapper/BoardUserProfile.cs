using AutoMapper;
using Board.Application.BoardUsers.AddMember;
using Board.Core.Entities;

namespace Board.Application.Mapper
{
	public class BoardUserProfile : Profile
	{
		public BoardUserProfile()
		{
			CreateMap<AddMemberCommand, BoardUser>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
		}
	}
}
