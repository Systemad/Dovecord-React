using Dovecord.Domain.Channels.Features;
using Dovecord.Domain.Servers.Features;
using FluentValidation;

namespace Dovecord.Domain.Servers.Validators;

public class CreateServerValidator : AbstractValidator<AddServer.AddServerCommand>
{
    public CreateServerValidator()
    {
        RuleFor(x => x.ServerToAdd.Name).NotEmpty();
        RuleFor(x => x.ServerToAdd.Name).Length(3, 15);
    }
}