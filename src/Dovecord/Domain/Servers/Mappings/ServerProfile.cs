using AutoMapper;
using Dovecord.Domain.Channels;
using Dovecord.Domain.Channels.Dto;
using Dovecord.Domain.Servers.Dto;

namespace Dovecord.Domain.Servers.Mappings;

public class ServerProfile : Profile
{
    public ServerProfile()
    {
        CreateMap<Server, ServerDto>().ReverseMap();
        CreateMap<ServerManipulationDto, Server>().ReverseMap();
    }
}