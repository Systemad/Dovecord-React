using Dovecord.Databases;
using Dovecord.Domain.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Dovecord.Domain.Users.Features;

public static class EnsureUserExists
{
    public record EnsureUserExistCommand(Guid UserId, string Username) : IRequest;

    public class QueryHandler : IRequestHandler<EnsureUserExistCommand>
    {
        private readonly DoveDbContext _context;
        private readonly IMediator _mediator;

        public QueryHandler(DoveDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(EnsureUserExistCommand request, CancellationToken cancellationToken)
        {
            var userExist = await 
                _mediator.Send(new DoesUserExist.DoesUserExistCommand(request.UserId), cancellationToken);

            if (userExist)
            {
                Log.Information("EnsureUserExists: User exist: {Bool}", userExist);
                return Unit.Value;
            }

            var addUser = new User
            {
                Id = request.UserId,
                Username = request.Username,
                IsOnline = true,
                Bot = false,
                System = false,
                AccentColor = null,
                Servers = null
            };

            _context.Add(addUser);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}