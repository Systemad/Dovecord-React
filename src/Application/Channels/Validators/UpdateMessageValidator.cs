using Application.Channels.Features;
using FluentValidation;

namespace Application.Channels.Validators;

public class UpdateChannelValidator : AbstractValidator<UpdateChannel.UpdateChannelCommand>
{
    public UpdateChannelValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Topic).Length(3, 15);
    }
}