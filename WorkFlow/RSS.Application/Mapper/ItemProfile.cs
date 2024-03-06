using AutoMapper;
using RSS.Application.DTOs;
using System.ServiceModel.Syndication;

namespace RSS.Application.Mapper
{
	public class ItemProfile : Profile
	{
		public ItemProfile()
		{
			CreateMap<SyndicationItem, Item>()
				.ForMember(dest => dest.Title, opt => opt
				.MapFrom(src => src.Title.Text))

				.ForMember(dest => dest.Uri, opt => opt
				.MapFrom(src => src.Links.FirstOrDefault()!.Uri.ToString()))

				.ForMember(dest => dest.PubDate, opt => opt
				.MapFrom(src => src.PublishDate.DateTime));
		}
	}
}
