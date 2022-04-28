using Dovecord.Domain.Channels.Features;
using FluentValidation;

namespace Dovecord.Domain.Channels.Validators;

public class CreateChannelValidator : AbstractValidator<AddServerChannel.AddServerChannelCommand>
{
    public CreateChannelValidator()
    {
        RuleFor(x => x.ChannelToAdd.Name).NotEmpty();
        RuleFor(x => x.ChannelToAdd.Name).Length(3, 15);
        
        //RuleFor(x => x.ChannelToAdd.ServerId).NotEmpty();
    }
}