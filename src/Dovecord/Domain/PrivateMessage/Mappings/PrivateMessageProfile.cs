using AutoMapper;
using Dovecord.Domain.Messages;
using Dovecord.Dtos.Message;
using Dovecord.Dtos.PrivateMessage;

namespace Dovecord.Domain.PrivateMessage.Mappings;

public class PrivateMessageProfile : Profile
{
    public PrivateMessageProfile()
    {
        CreateMap<PrivateMessage, PrivateMessageDto>().ReverseMap();
        CreateMap<PrivateMessageDto, PrivateMessage>().ReverseMap();
    }
}