using AutoMapper;
using Infrastructure.Dtos.Channel;
using Infrastructure.Dtos.Message;
using WebUI.Domain.Messages;

namespace Application.Common.Mappings;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<ChannelMessage, ChannelMessageDto>().ReverseMap();
        CreateMap<ChannelManipulationDto, ChannelMessage>().ReverseMap();
    }
}