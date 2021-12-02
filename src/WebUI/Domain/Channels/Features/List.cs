using Domain.Channels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;

namespace WebUI.Domain.Channels.Features;

public class List
{
    public record Query : IRequest<List<Channel>>;

    public class QueryHandler : IRequestHandler<Query, List<Channel>>
    {
        private DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Channel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var channels = await _context.Channels.ToListAsync(cancellationToken);
            return channels;
        }
    }
}