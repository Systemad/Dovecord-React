using Application.Servers.Features;
using FluentValidation;

namespace Application.Servers.Validators;


public class CreateServerValidator : AbstractValidator<AddServer.AddServerCommand>
{
    public CreateServerValidator()
    {
        RuleFor(x => x.Server.Name).NotEmpty();
        RuleFor(x => x.Server.Name).Length(3, 15);
    }
}
