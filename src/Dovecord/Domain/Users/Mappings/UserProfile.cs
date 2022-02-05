using AutoMapper;
using Dovecord.Dtos.User;
using Dovecord.Domain.Channels;
using Dovecord.Dtos.Channel;

namespace Dovecord.Domain.Users.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserCreationDto, User>().ReverseMap();
        CreateMap<UserManipulationDto, User>().ReverseMap();
    }
}