using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Messages;

public class List
{
    public record Query(Guid Id) : IRequest<List<ChannelMessage>>;

    public class QueryHandler : IRequestHandler<Query, List<ChannelMessage>>
    {
        private DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<ChannelMessage>> Handle(Query request, CancellationToken cancellationToken)
        {
            var messages = await _context.Channels
                .Where(a => a.ChannelId == request.Id)
                .Include(m => m.ChannelMessages)
                .FirstOrDefaultAsync(cancellationToken);
                //.ToListAsync(cancellationToken);
            
            
            // TODO: Wrong
            var channels = await _context.ChannelMessages
                .Where(a => a.ChannelMessageId == request.Id)
                .ToListAsync(cancellationToken);
            
            return channels;
        }
    }
}