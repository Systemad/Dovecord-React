using Application.Database;
using Domain.Users;
using Domain.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Features;

public static class AddUser
{
    public record AddUserCommand(Guid Id, string Username) : IRequest<UserDto>;

    public class Handler : IRequestHandler<AddUserCommand, UserDto>
    {
        private readonly DoveDbContext _context;

        public Handler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = request.Id,
                Username = request.Username,
                Bot = false,
                System = false,
                AccentColor = null,
                LastOnline = DateTime.Now
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var query = await _context.Users.Where(u => u.Id == request.Id)
                .Select(us => new UserDto
                {
                    Id = us.Id,
                    Username = us.Username,
                    Bot = us.Bot,
                    System = us.System,
                    AccentColor = us.AccentColor,
                    LastOnline = us.LastOnline
                }).SingleOrDefaultAsync(cancellationToken);
            return query;
        }
    }
}