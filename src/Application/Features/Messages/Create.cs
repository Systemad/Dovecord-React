using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;

namespace Application.Features.Messages;

public class Create
{
    public record Command(ChannelMessage Message) : IRequest<ChannelMessage>;

    public class QueryHandler : IRequestHandler<Command, ChannelMessage>
    {
        private DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<ChannelMessage> Handle(Command request, CancellationToken cancellationToken)
        {
            
            //var channel = await _context.TextChannels.FirstAsync(x => x.Name == request.Name, cancellationToken);
            
            //if (channel is not null)
            //    return new ArgumentNullException();
            var newmessage = new ChannelMessage
            {
                ChannelMessageId = Guid.NewGuid(),
                //UserId = request.Message.UserId,
                Content = request.Message.Content,
                //CreatedAt = DateTime.Now
            };
            await _context.AddAsync(newmessage, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newmessage;
        }
    }
}