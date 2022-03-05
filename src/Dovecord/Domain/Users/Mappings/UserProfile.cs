using AutoMapper;
using Dovecord.Domain.Channels;
using Dovecord.Domain.Users.Dto;

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