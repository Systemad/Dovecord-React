using Dovecord.Domain.Channels.Features;
using Dovecord.Domain.Servers.Features;
using FluentValidation;

namespace Dovecord.Domain.Servers.Validators;

public class UpdateServerValidator : AbstractValidator<UpdateServer.UpdateServerCommand>
{
    public UpdateServerValidator()
    {
        RuleFor(x => x.NewServerData.Name).NotEmpty();
        RuleFor(x => x.NewServerData.Name).Length(3, 15);
    }
}