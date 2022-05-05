using Application.Servers.Features;
using FluentValidation;

namespace Application.Servers.Validators;


public class CreateServerValidator : AbstractValidator<AddServer.AddServerCommand>
{
    public CreateServerValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).Length(3, 15);
    }
}
