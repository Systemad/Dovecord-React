using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;

namespace WebUI.Domain.Messages.Features;

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
                .Where(a => a.Id == request.Id)
                .Include(m => m.ChannelMessages)
                .FirstOrDefaultAsync(cancellationToken);
                //.ToListAsync(cancellationToken);
            
            
            // TODO: Wrong
            var channels = await _context.ChannelMessages
                .Where(a => a.Id == request.Id)
                .ToListAsync(cancellationToken);
            
            return channels;
        }
    }
}