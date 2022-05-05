using Application.Database;
using Domain.Servers.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class GetServerList
{
    public record ServerListQuery : IRequest<List<ServerDto>>;

    public class QueryHandler : IRequestHandler<ServerListQuery, List<ServerDto>>
    {
        private readonly DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<ServerDto>> Handle(ServerListQuery request, CancellationToken cancellationToken)
        {
            var servers = await _context.Servers
                .Select(s => new ServerDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    IconUrl = s.IconUrl,
                    OwnerUserId = s.OwnerUserId
                })
                .ToListAsync(cancellationToken);

            return servers;
        }
    }
}