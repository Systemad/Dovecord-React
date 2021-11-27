using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;

namespace Application.Features.Messages;

public class Create
{
    public record Command(Message Message) : IRequest<Message>;

    public class QueryHandler : IRequestHandler<Command, Message>
    {
        private DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<Message> Handle(Command request, CancellationToken cancellationToken)
        {
            
            //var channel = await _context.TextChannels.FirstAsync(x => x.Name == request.Name, cancellationToken);
            
            //if (channel is not null)
            //    return new ArgumentNullException();
            var newmessage = new Message
            {
                Id = Guid.NewGuid(),
                UserId = request.Message.UserId,
                Content = request.Message.Content,
                CreatedAt = DateTime.Now
            };
            await _context.AddAsync(newmessage, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newmessage;
        }
    }
}