using Dovecord.Domain.Users.Dto;
using Dovecord.Domain.Users.Features;
using MediatR;

namespace Dovecord.Domain.Users.Services;

public class StatusService : IStatusService
{
    private readonly IMediator _mediator;
    
    public StatusService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task OnStartSession(Guid userId, string? username)
    {
        var updateUser = new UpdateUser.UpdateUserCommand(userId, new UserManipulationDto { IsOnline = true });
        var userExist = await _mediator.Send(updateUser);
        if (!userExist)
        {
            var addUser = new AddUser.AddUserCommand(new UserCreationDto
            {
                IsOnline = true
            });
            await _mediator.Send(addUser);
        }
    }

    public async Task OnStopSession(Guid userId)
    {
        var updateUser = new UpdateUser.UpdateUserCommand(userId, new UserManipulationDto { IsOnline = false });
        await _mediator.Send(updateUser);
    }
}