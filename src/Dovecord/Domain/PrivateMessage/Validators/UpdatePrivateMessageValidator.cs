using Dovecord.Domain.PrivateMessage.Features;
using FluentValidation;

namespace Dovecord.Domain.PrivateMessage.Validators;

public class UpdatePrivateMessageValidator : AbstractValidator<UpdatePrivateMessage.UpdatePrivateMessageCommand>
{
    public UpdatePrivateMessageValidator()
    {
        RuleFor(x => x.NewMessageData).NotEmpty();
        RuleFor(x => x.NewMessageData).Length(1, 200);
    }
}