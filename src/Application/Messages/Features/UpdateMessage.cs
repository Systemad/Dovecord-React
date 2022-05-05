using Application.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Features;

public static class UpdateMessage
{
    public record UpdateMessageCommand(Guid Id, string NewMessageData, Guid InvokerUserId) : IRequest<bool>;
    
    public class Query : IRequestHandler<UpdateMessageCommand, bool>
    {
        private readonly DoveDbContext _context;

        public Query(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var messageToUpdate = await _context.ChannelMessages
                .Where(x => x.Id == request.Id)
                .Include(a => a.Author)
                .AsTracking()
                .SingleOrDefaultAsync(cancellationToken);
            
            if (messageToUpdate is null)
                throw new NotFoundException("Message", request.Id);
            
            if (request.InvokerUserId != messageToUpdate.Author.Id)
                return false;

            messageToUpdate.IsEdit = true;
            messageToUpdate.LastModifiedOn = DateTime.Now;
            messageToUpdate.Content = request.NewMessageData;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}