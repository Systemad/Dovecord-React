using Application.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Channels.Features;

public static class UpdateChannel
{
    public record UpdateChannelCommand(Guid Id, string Name, string Topic) : IRequest<bool>;
    
    public class Query : IRequestHandler<UpdateChannelCommand, bool>
    {
        private readonly DoveDbContext _context;
        
        public Query(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateChannelCommand request, CancellationToken cancellationToken)
        {
            var channelToUpdate = await _context.Channels
                .Where(x => x.Id == request.Id)
                .AsTracking()
                .FirstOrDefaultAsync(cancellationToken);
            
            if (channelToUpdate is null)
                throw new NotFoundException("Channel", request.Id);
            
            channelToUpdate.Name = request.Name;
            channelToUpdate.Topic = request.Topic;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}