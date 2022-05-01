using Application.Servers.Features;
using FluentValidation;

namespace Application.Servers.Validators;

public class CreateServerValidator : AbstractValidator<AddServer.AddServerCommand>
{
    public CreateServerValidator()
    {
        RuleFor(x => x.ServerCreatedEvent.Name).NotEmpty();
        RuleFor(x => x.ServerCreatedEvent.Name).Length(3, 15);
    }
}