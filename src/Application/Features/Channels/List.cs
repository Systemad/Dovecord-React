using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Channels;

public class List
{
    public record Query : IRequest<List<TextChannel>>;

    public class QueryHandler : IRequestHandler<Query, List<TextChannel>>
    {
        private DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<TextChannel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var channels = await _context.TextChannels.ToListAsync(cancellationToken);
            return channels;
        }
    }

}