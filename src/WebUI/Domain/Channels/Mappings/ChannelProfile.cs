using AutoMapper;
using Infrastructure.Dtos.Channel;
using Domain.Channels;
using Channel = WebUI.Domain.Channels.Channel;

namespace Application.Common.Mappings;

public class ChannelProfile : Profile
{
    public ChannelProfile()
    {
        CreateMap<Channel, ChannelDto>().ReverseMap();
        CreateMap<ChannelManipulationDto, Channel>().ReverseMap();
    }
}