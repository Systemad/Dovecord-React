using Dovecord.Databases;
using Dovecord.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Servers.Features;

public static class DeleteServer
{
    public record DeleteServerCommand(Guid Id) : IRequest<bool>;
    
    public class Handler : IRequestHandler<DeleteServerCommand, bool>
    {
        private readonly DoveDbContext _context;
        public Handler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteServerCommand request, CancellationToken cancellationToken)
        {
            var server = await _context.Servers
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (server is null)
                throw new NotFoundException("Channel", request.Id);

            _context.Servers.Remove(server);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}