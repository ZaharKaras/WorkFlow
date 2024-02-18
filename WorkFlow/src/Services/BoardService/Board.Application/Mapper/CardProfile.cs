using AutoMapper;
using Board.Application.Cards.Create;
using Board.Application.Cards.DTOs;
using Board.Core.Entities;


namespace Board.Application.Mapper
{
	public class CardProfile : Profile
	{
		public CardProfile()
		{
			CreateMap<CreateCardCommand, Card>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

			CreateMap<Card, CardDTO>();

			CreateMap<Card, CardListDTO>();
		}
	}
}
