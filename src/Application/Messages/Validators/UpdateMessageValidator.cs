using Application.Messages.Features;
using FluentValidation;

namespace Application.Messages.Validators;

public class UpdateMessageValidator : AbstractValidator<UpdateMessage.UpdateMessageCommand>
{
    public UpdateMessageValidator()
    {
        RuleFor(x => x.NewMessageData).NotEmpty();
        RuleFor(x => x.NewMessageData).Length(1, 200);
    }
}