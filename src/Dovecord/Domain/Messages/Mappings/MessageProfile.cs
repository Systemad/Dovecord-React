using AutoMapper;
using Dovecord.Domain.Messages.Dto;

namespace Dovecord.Domain.Messages.Mappings;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<ChannelMessage, ChannelMessageDto>().ReverseMap();
        CreateMap<MessageManipulationDto, ChannelMessage>().ReverseMap();
    }
}