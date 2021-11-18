using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Channels;

public class Create
{

    public record Command(TextChannel TextChannel) : IRequest<TextChannel>;

    public class CommandValidator : AbstractValidator<TextChannel>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }

    public class QueryHandler : IRequestHandler<Command, TextChannel>
    {
        private DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<TextChannel> Handle(Command request, CancellationToken cancellationToken)
        {
            
            var channel = await _context.TextChannels.FirstAsync(x => x.Name == request.TextChannel.Name, cancellationToken);
            
            //if (channel is not null)
            //    return new ArgumentNullException();
            
            var newchannel = new TextChannel
            {
                Id = Guid.NewGuid(),
                Name = request.TextChannel.Name,
            };
            await _context.SaveChangesAsync(cancellationToken);
            return newchannel;
        }
    }
}