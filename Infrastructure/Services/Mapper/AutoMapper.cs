using AutoMapper;
using Core.Entities.Identity;
using Infrastructure.Dtos.Identity;

namespace Infrastructure.Services.Mapper;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<RegisterDto, AppUser>()
            .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.UserName.ToLower()))
            .ReverseMap();

        CreateMap<UserDto, AppUser>()
            .ReverseMap();
    }
}
