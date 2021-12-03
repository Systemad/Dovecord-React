using AutoMapper;
using WebUI.Domain.Messages;
using WebUI.Dtos.Channel;
using WebUI.Dtos.Message;

namespace Application.Common.Mappings;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<ChannelMessage, ChannelMessageDto>().ReverseMap();
        CreateMap<ChannelManipulationDto, ChannelMessage>().ReverseMap();
    }
}