using AutoMapper;
using Domain.Servers;
using Domain.Servers.Dto;

namespace Application.Servers.Mappings;

public class ServerProfile : Profile
{
    public ServerProfile()
    {
        CreateMap<Server, ServerDto>().ReverseMap();
        CreateMap<CreateServerModel, Server>().ReverseMap();
    }
}