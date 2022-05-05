using Application.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Features;

public static class DoesUserExist
{
    public record DoesUserExistCommand(Guid UserId) : IRequest<bool>;

    public class QueryHandler : IRequestHandler<DoesUserExistCommand, bool>
    {
        private readonly DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DoesUserExistCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _context.Users.AnyAsync(u => u.Id == request.UserId, cancellationToken);
            return userExist;
        }
    }
}