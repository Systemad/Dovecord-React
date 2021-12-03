using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;
using WebUI.Exceptions;

namespace WebUI.Domain.Channels.Features;

public static class DeleteChannel
{
    public record DeleteChannelCommand(Guid Id) : IRequest<bool>;
    
    public class Handler : IRequestHandler<DeleteChannelCommand, bool>
    {
        private readonly DoveDbContext _context;
        public Handler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = await _context.Channels
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (channel is null)
                throw new NotFoundException("Channel", request.Id);

            
            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}