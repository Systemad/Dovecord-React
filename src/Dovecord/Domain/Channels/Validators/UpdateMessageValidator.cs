using Dovecord.Domain.Channels.Features;
using Dovecord.Domain.Messages.Features;
using FluentValidation;

namespace Dovecord.Domain.Channels.Validators;

public class UpdateChannelValidator : AbstractValidator<UpdateChannel.UpdateChannelCommand>
{
    public UpdateChannelValidator()
    {
        RuleFor(x => x.NewChannelData.Name).NotEmpty();
        RuleFor(x => x.NewChannelData.Name).Length(3, 15);
    }
}