using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Channels;

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