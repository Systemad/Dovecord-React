using Application.Messages.Features;
using FluentValidation;

namespace Application.Messages.Validators;

public class CreateMessageValidator : AbstractValidator<AddMessage.AddMessageCommand>
{
    public CreateMessageValidator()
    {
        RuleFor(x => x.ChannelId).NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.Content).Length(1, 200);
    }
}