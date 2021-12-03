using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;
using WebUI.Exceptions;

namespace WebUI.Domain.Messages.Features;

public static class DeleteMessage
{
    public record DeleteMessageCommand(Guid Id) : IRequest<bool>;
    
    public class Handler : IRequestHandler<DeleteMessageCommand, bool>
    {
        private readonly DoveDbContext _context;
        public Handler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _context.ChannelMessages
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (message is null)
                throw new NotFoundException("Message", request.Id);
            
            _context.ChannelMessages.Remove(message);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}