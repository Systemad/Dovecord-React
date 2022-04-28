using AutoMapper;
using Domain.Channels;
using Domain.Channels.Dto;
using Domain.Servers;
using Domain.Servers.Dto;

namespace Dovecord.Domain.Servers.Mappings;

public class ServerProfile : Profile
{
    public ServerProfile()
    {
        CreateMap<Server, ServerDto>().ReverseMap();
        CreateMap<CreateServerModel, Server>().ReverseMap();
    }
}