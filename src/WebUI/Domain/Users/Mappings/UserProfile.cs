using AutoMapper;
using Infrastructure.Dtos.User;
using WebUI.Domain.Channels;
using WebUI.Dtos.Channel;
using WebUI.Dtos.User;

namespace WebUI.Domain.Users.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserManipulationDto, User>().ReverseMap();
    }
}