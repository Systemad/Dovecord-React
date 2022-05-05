using Application.Database;
using Domain.Servers.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class GetServersOfUser
{
    public record GetServersOfUserQuery(Guid UserId) : IRequest<List<ServerDto>>;

    public class QueryHandler : IRequestHandler<GetServersOfUserQuery, List<ServerDto>>
    {
        private readonly DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServerDto>> Handle(GetServersOfUserQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = request.UserId;
            var filteredServer = await _context.Users
                .Where(user => user.Id == currentUserId)
                //.Include(x => x.Servers.Select(x => x.Channels))
                //.Include(x => x.Servers.Select(x => x.Members))
                .Select(servers => servers.Servers
                    .Select(s => new ServerDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        IconUrl = s.IconUrl,
                        OwnerUserId = s.OwnerUserId
                    }).ToList())
                .FirstOrDefaultAsync( cancellationToken);

            return filteredServer;
        }
    }
}