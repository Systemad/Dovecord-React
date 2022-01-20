using Dovecord.Domain.Messages.Features;
using Dovecord.Dtos.Message;
using FluentValidation;

namespace Dovecord.Domain.Messages.Validators;

public class UpdateMessageValidator : AbstractValidator<UpdateMessage.UpdateMessageCommand>
{
    public UpdateMessageValidator()
    {
        RuleFor(x => x.NewMessageData).NotEmpty();
        RuleFor(x => x.NewMessageData).Length(1, 200);
    }
}