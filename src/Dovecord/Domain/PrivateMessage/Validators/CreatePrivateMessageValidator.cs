using Dovecord.Domain.Messages.Features;
using Dovecord.Domain.PrivateMessage.Features;
using FluentValidation;

namespace Dovecord.Domain.PrivateMessage.Validators;

public class CreatePrivateMessageValidator : AbstractValidator<AddPrivateMessage.AddPrivateMessageCommand>
{
    public CreatePrivateMessageValidator()
    {
        RuleFor(x => x.MessageToAdd.ReceiverId).NotEmpty();
        RuleFor(x => x.MessageToAdd.Content).NotEmpty();
        RuleFor(x => x.MessageToAdd.Content).Length(1, 200);
    }
}