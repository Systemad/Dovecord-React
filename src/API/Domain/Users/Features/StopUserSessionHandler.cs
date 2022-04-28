using Infrastructure.Database;
using Domain.Users.Dto;
using Dovecord.Services;
using MediatR;
using Serilog;

namespace Dovecord.Domain.Users.Features;

public static class StopUserSessionHandler
{
    public record StopUserSessionCommand(Guid UserId, string Username) : IRequest, IEnsureUserExistsRequest;
    
    public class Handler : IRequestHandler<StopUserSessionCommand>
    {
        private readonly DoveDbContext _context;
        private readonly IMediator _mediator;

        public Handler(DoveDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(StopUserSessionCommand request, CancellationToken cancellationToken)
        {
            Log.Information("StartUserSession handler");
            var updateUser =
                new UpdateUser.UpdateUserCommand(request.UserId, new UserManipulationDto {  });
            await _mediator.Send(updateUser, cancellationToken);
            //IRequest 
            /*if (!userExist)
            {
                var addUser = new AddUser.AddUserCommand(new UserCreationDto
                {
                    IsOnline = false
                });
                await _mediator.Send(addUser, cancellationToken);
            }
            */
            return Unit.Value;
        }
    }
}
