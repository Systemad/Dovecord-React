using Application.Database;
using Domain.Users;
using MediatR;

namespace Application.Users.Features;

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
                return Unit.Value;
            }

            var addUser = new User
            {
                Id = request.UserId,
                Username = request.Username,
                Bot = false,
                System = false,
                AccentColor = null,
                Servers = null
            };

            _context.Users.Add(addUser);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}