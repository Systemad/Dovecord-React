using Application.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class AddUserToServer
{
    public record AddUserToServerCommand(Guid ServerId, Guid UserId) : IRequest<bool>;
    
    public class Query : IRequestHandler<AddUserToServerCommand, bool>
    {
        private readonly DoveDbContext _context;

        public Query(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddUserToServerCommand request, CancellationToken cancellationToken)
        {
            var serverToUpdate = await _context.Servers
                .Where(x => x.Id == request.ServerId)
                .Include(m => m.Members)
                .AsTracking()
                .FirstAsync(cancellationToken);

            var member = await _context.Users
                .AsTracking()
                .FirstAsync(m => m.Id == request.UserId, cancellationToken);
            
            /*
            if (member is null)
                throw new NotFoundException("Member", member);
            
            if (serverToUpdate is null)
                throw new NotFoundException("Server", serverToUpdate);
            */
            serverToUpdate.Members.Add(member);
            
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}