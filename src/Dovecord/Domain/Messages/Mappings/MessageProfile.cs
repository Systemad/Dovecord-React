using AutoMapper;
using Dovecord.Dtos.Message;
using Dovecord.Dtos.Channel;

namespace Dovecord.Domain.Messages.Mappings;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<ChannelMessage, ChannelMessageDto>().ReverseMap();
        CreateMap<MessageManipulationDto, ChannelMessage>().ReverseMap();
    }
}