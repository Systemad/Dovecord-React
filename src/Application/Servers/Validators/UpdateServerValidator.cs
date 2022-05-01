using Application.Servers.Features;
using FluentValidation;

namespace Application.Servers.Validators;

public class UpdateServerValidator : AbstractValidator<UpdateServer.UpdateServerCommand>
{
    public UpdateServerValidator()
    {
        RuleFor(x => x.NewCreateServerData.Name).NotEmpty();
        RuleFor(x => x.NewCreateServerData.Name).Length(3, 15);
    }
}