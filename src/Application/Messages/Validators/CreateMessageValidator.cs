using Application.Messages.Features;
using FluentValidation;

namespace Application.Messages.Validators;

public class CreateMessageValidator : AbstractValidator<AddMessage.AddMessageCommandM>
{
    public CreateMessageValidator()
    {
        RuleFor(x => x.Message.ChannelId).NotEmpty();
        RuleFor(x => x.Message.Content).NotEmpty();
        RuleFor(x => x.Message.Content).Length(1, 200);
    }
}