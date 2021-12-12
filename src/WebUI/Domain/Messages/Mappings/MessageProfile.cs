using AutoMapper;
using WebUI.Dtos.Channel;
using WebUI.Dtos.Message;

namespace WebUI.Domain.Messages.Mappings;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<ChannelMessage, ChannelMessageDto>().ReverseMap();
        CreateMap<MessageManipulationDto, ChannelMessage>().ReverseMap();
    }
}