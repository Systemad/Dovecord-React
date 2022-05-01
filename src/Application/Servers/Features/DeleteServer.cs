using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class DeleteServer
{
    public record DeleteServerCommand(Guid Id, Guid UserId) : IRequest<bool>;
    
    public class Handler : IRequestHandler<DeleteServerCommand, bool>
    {
        private readonly IDoveDbContext _context;
        public Handler(IDoveDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteServerCommand request, CancellationToken cancellationToken)
        {
            var server = await _context.Servers
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (server is null)
                throw new NotFoundException("Channel", request.Id);

            if(server.OwnerUserId != request.UserId)
                throw new NotFoundException("Channel", request.Id);
            
            _context.Servers.Remove(server);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}