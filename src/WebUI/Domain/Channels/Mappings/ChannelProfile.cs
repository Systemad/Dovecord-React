using AutoMapper;
using Infrastructure.Dtos.Channel;

namespace WebUI.Domain.Channels.Mappings;

public class ChannelProfile : Profile
{
    public ChannelProfile()
    {
        CreateMap<Channel, ChannelDto>().ReverseMap();
        CreateMap<ChannelManipulationDto, Channel>().ReverseMap();
    }
}