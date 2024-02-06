using AutoMapper;
using Identity.Core.Entities;
using Identity.Infrastructure.DTOs;

namespace Identity.Infrastructure.Mapper
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserResponse>();
			CreateMap<UserResponse, User>();
		}
	}
}
