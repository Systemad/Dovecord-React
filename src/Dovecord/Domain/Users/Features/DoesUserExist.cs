using DataAccess.Database;
using Dovecord.Domain.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Dovecord.Domain.Users.Features;

public static class DoesUserExist
{
    public record DoesUserExistCommand(Guid UserId) : IRequest<bool>;

    public class QueryHandler : IRequestHandler<DoesUserExistCommand, bool>
    {
        private readonly DoveDbContext _context;
        private readonly IMediator _mediator;

        public QueryHandler(DoveDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DoesUserExistCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _context.Users.AnyAsync(u => u.Id == request.UserId, cancellationToken);
            Log.Information("DoesUserExist: User exist: {Bool}", userExist);
            return userExist;
        }
    }
}