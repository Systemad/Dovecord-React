using Application.Channels.Features;
using FluentValidation;

namespace Application.Channels.Validators;

public class CreateChannelValidator : AbstractValidator<AddServerChannel.AddServerChannelCommand>
{
    public CreateChannelValidator()
    {
        RuleFor(x => x.Channel.Name).NotEmpty();
        RuleFor(x => x.Channel.Name).Length(3, 15);
    }
}