using Application.Channels.Features;
using FluentValidation;

namespace Application.Channels.Validators;

public class UpdateChannelValidator : AbstractValidator<UpdateChannel.UpdateChannelCommand>
{
    public UpdateChannelValidator()
    {
        RuleFor(x => x.NewChannelData.Name).NotEmpty();
        RuleFor(x => x.NewChannelData.Name).Length(3, 15);
    }
}