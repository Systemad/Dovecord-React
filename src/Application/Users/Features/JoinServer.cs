using Application.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Features;

public static class JoinServer
{
    public record JoinServerCommand(Guid ServerId, Guid UserId) : IRequest<bool>;
    
    public class Query : IRequestHandler<JoinServerCommand, bool>
    {
        private readonly DoveDbContext _context;

        public Query(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(JoinServerCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _context.Users
                .Where(x => x.Id == request.UserId)
                .Include(m => m.Servers)
                .AsTracking()
                .FirstAsync(cancellationToken);

            var server = await _context.Servers
                .AsTracking()
                .FirstAsync(m => m.Id == request.ServerId, cancellationToken);
            
            /*
            if (member is null)
                throw new NotFoundException("Member", member);
            
            if (serverToUpdate is null)
                throw new NotFoundException("Server", serverToUpdate);
            */
            userToUpdate.Servers.Add(server);
            
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}