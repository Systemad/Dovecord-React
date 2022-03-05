using AutoMapper;
using Dovecord.Domain.Channels.Dto;

namespace Dovecord.Domain.Channels.Mappings;

public class ChannelProfile : Profile
{
    public ChannelProfile()
    {
        CreateMap<Channel, ChannelDto>().ReverseMap();
        CreateMap<ChannelManipulationDto, Channel>().ReverseMap();
    }
}