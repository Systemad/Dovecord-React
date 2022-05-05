using Application.Database;
using Domain.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class GetServerUsers
{
    public record GetServerUsersQuery(Guid serverId) : IRequest<List<UserDto>>;

    public class QueryHandler : IRequestHandler<GetServerUsersQuery, List<UserDto>>
    {
        private readonly DoveDbContext _context;
        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> Handle(GetServerUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Servers
                .Where(server => server.Id == request.serverId)
                .Select(servers => servers.Members
                    .Select(m => new UserDto
                    {
                        Id = m.Id,
                        Username = m.Username,
                        Bot = m.Bot,
                        System = m.System,
                        AccentColor = m.AccentColor,
                        LastOnline = m.LastOnline,
                    }).ToList())
                .FirstOrDefaultAsync(cancellationToken);
            
            return users;
        }
    }
}