using AutoMapper;
using Domain.Messages.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Features;

public static class GetMessagesFromChannel
{
    public record MessageListQuery(Guid id) : IRequest<List<ChannelMessageDto>>;

    public class QueryHandler : IRequestHandler<MessageListQuery, List<ChannelMessageDto>>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(IDoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<ChannelMessageDto>> Handle(MessageListQuery request, CancellationToken cancellationToken)
        {
            var messages = await _context.Channels
                .Where(channel => channel.Id == request.id)
                .Select(msg => msg.Messages)
                .FirstOrDefaultAsync(cancellationToken);
            //var messages = await _context.ChannelMessages
            //    .Where(m => m.ChannelId == request.id)
            //    .ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<ChannelMessageDto>>(messages);
        }
    }
}