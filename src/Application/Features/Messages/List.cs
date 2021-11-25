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
            var channels = await _context.ChannelMessages
                .Where(a => a.TextChannelId == request.Id)
                .ToListAsync(cancellationToken);
            return channels;
        }
    }
}