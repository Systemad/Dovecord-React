using Dovecord.Databases;
using Dovecord.Domain.Users.Dto;
using Dovecord.Services;
using MediatR;
using Serilog;

namespace Dovecord.Domain.Users.Features;


public static class StartUserSessionHandler
{
    public record StartUserSessionCommand(Guid UserId, string Username) : IRequest, IEnsureUserExistsRequest;
    
    public class Handler : IRequestHandler<StartUserSessionCommand>
    {
        private readonly DoveDbContext _context;
        private readonly IMediator _mediator;

        public Handler(DoveDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(StartUserSessionCommand request, CancellationToken cancellationToken)
        {
            Log.Information("StartUserSession handler");
            var updateUser =
                new UpdateUser.UpdateUserCommand(request.UserId, new UserManipulationDto { });
            var userExist = await _mediator.Send(updateUser, cancellationToken);
            //IRequest type = IEns
            /*if (!userExist)
            {
                var addUser = new AddUser.AddUserCommand(new UserCreationDto
                {
                    IsOnline = true
                });
                await _mediator.Send(addUser, cancellationToken);
            }
            */
            return Unit.Value;
        }
    }
}
