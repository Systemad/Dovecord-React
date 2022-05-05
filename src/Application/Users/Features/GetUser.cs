using Application.Database;
using Domain.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Features;

public static class GetUser
{
    public record GetUserQuery(Guid Id) : IRequest<UserDto>;

    public class QueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var query = await _context.Users
                .Where(user => user.Id == request.Id)
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

            if(query is null)
                throw new NotFoundException("User", request.Id);
            
            return query;
        }
    }
}