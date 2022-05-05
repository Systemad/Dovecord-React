using Application.Database;
using Domain.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Features;

public static class GetMe
{
    public record GetMeQuery(Guid UserId) : IRequest<UserDto>;

    public class QueryHandler : IRequestHandler<GetMeQuery, UserDto>
    {
        private readonly DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(GetMeQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = request.UserId;
            var query = await _context.Users
                .Where(user => user.Id == currentUserId)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Bot = u.Bot,
                    System = u.System,
                    AccentColor = u.AccentColor,
                    LastOnline = u.LastOnline,
                })
                .FirstOrDefaultAsync(cancellationToken);
            
            return query;
        }
    }
}