using Application.Messages.Features;
using FluentValidation;

namespace Application.Messages.Validators;

public class CreateMessageValidator : AbstractValidator<AddMessage.AddMessageCommand>
{
    public CreateMessageValidator()
    {
        RuleFor(x => x.MessageToAdd.ChannelId).NotEmpty();
        RuleFor(x => x.MessageToAdd.Content).NotEmpty();
        RuleFor(x => x.MessageToAdd.Content).Length(1, 200);
    }
}