using Application.Database;
using Domain.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Features;

public static class GetUserList
{
    public record UserListQuery : IRequest<List<UserDto>>;

    public class QueryHandler : IRequestHandler<UserListQuery, List<UserDto>>
    {
        private readonly DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<UserDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            var query = await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Bot = u.Bot,
                    System = u.System,
                    AccentColor = u.AccentColor,
                    LastOnline = u.LastOnline,
                })
                .ToListAsync(cancellationToken);
            return query;
        }
    }
}