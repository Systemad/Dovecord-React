using AutoMapper;
using Domain.Channels;
using Domain.Channels.Dto;

namespace Application.Channels.Mappings;

public class ChannelProfile : Profile
{
    public ChannelProfile()
    {
        CreateMap<Channel, ChannelDto>().ReverseMap();
        CreateMap<ChannelManipulationDto, Channel>().ReverseMap();
    }
}