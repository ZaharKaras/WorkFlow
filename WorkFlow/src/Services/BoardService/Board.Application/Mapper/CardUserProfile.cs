using AutoMapper;
using Board.Application.CardUsers.AddAssignee;
using Board.Core.Entities;

namespace Board.Application.Mapper
{
	public class CardUserProfile : Profile
	{
		public CardUserProfile()
		{
			CreateMap<AddAssigneeCommand, CardUser>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
		}
	}
}
