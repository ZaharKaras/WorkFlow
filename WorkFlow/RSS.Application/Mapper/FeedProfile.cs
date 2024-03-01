using AutoMapper;
using RSS.Application.DTOs;
using RSS.Core.Entities;
using System.ServiceModel.Syndication;

namespace RSS.Application.Mapper
{
	public class FeedProfile : Profile
	{
		public FeedProfile()
		{
			CreateMap<SyndicationFeed, Feed>()
				.ForMember(dest => dest.Id, opt => opt
				.MapFrom(src => Guid.NewGuid()))

				.ForMember(dest => dest.Link, opt => opt
				.MapFrom(src => src.BaseUri))

				.ForMember(dest => dest.Title, opt => opt
				.MapFrom(src => src.Title.Text));

			CreateMap<Feed, FeedDTO>();
		}
	}
}
