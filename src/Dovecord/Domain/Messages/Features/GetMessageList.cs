using AutoMapper;
using Dovecord.Databases;
using Dovecord.Dtos.Message;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Messages.Features;

public static class GetMessageList
{
    public record MessageListQuery(Guid id) : IRequest<List<ChannelMessageDto>>;

    public class QueryHandler : IRequestHandler<MessageListQuery, List<ChannelMessageDto>>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<ChannelMessageDto>> Handle(MessageListQuery request, CancellationToken cancellationToken)
        {
            var messages = await _context.ChannelMessages
                .Where(m => m.ChannelId == request.id).ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<ChannelMessageDto>>(messages);
        }
    }
}