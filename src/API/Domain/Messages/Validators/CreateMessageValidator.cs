using Dovecord.Domain.Messages.Features;
using FluentValidation;

namespace Dovecord.Domain.Messages.Validators;

public class CreateMessageValidator : AbstractValidator<AddMessage.AddMessageCommand>
{
    public CreateMessageValidator()
    {
        RuleFor(x => x.MessageToAdd.ChannelId).NotEmpty();
        RuleFor(x => x.MessageToAdd.Content).NotEmpty();
        RuleFor(x => x.MessageToAdd.Content).Length(1, 200);
    }
}