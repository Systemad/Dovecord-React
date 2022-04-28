using Dovecord.Domain.Servers.Features;
using FluentValidation;

namespace Dovecord.Domain.Servers.Validators;

public class CreateServerValidator : AbstractValidator<AddServer.AddServerCommand>
{
    public CreateServerValidator()
    {
        RuleFor(x => x.ServerCreatedEvent.Name).NotEmpty();
        RuleFor(x => x.ServerCreatedEvent.Name).Length(3, 15);
    }
}