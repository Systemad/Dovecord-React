using Application.Database;
using Domain.Servers;
using Domain.Servers.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class AddServer
{
    public record AddServerCommand(Server Server) : IRequest<ServerDto>;

    public class Handler : IRequestHandler<AddServerCommand, ServerDto>
    {
        private readonly DoveDbContext _context;

        public Handler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<ServerDto> Handle(AddServerCommand request, CancellationToken cancellationToken)
        {

            _context.Servers.Add(request.Server);
            await _context.SaveChangesAsync(cancellationToken);
            
            var serverToUpdate = await _context.Servers
                .Where(x => x.Id == request.Server.Id)
                .Include(m => m.Members)
                .AsTracking()
                .FirstAsync(cancellationToken);

            var member = await _context.Users
                .AsTracking()
                .FirstAsync(m => m.Id == request.Server.OwnerUserId, cancellationToken);
            
            serverToUpdate.Members.Add(member);
            await _context.SaveChangesAsync(cancellationToken);

            var query = await _context.Servers.Where(s => s.Id == request.Server.Id)
                .Select(sv => new ServerDto
                {
                    Id = sv.Id,
                    Name = sv.Name,
                    IconUrl = sv.IconUrl,
                    OwnerUserId = sv.OwnerUserId
                }).SingleOrDefaultAsync(cancellationToken);
            
            return query;
        }
    }
}