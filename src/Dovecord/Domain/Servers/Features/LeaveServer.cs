using AutoMapper;
using DataAccess.Database;
using Dovecord.Domain.Servers.Dto;
using Dovecord.Domain.Users.Dto;
using Dovecord.Domain.Users.Features;
using Dovecord.Exceptions;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Servers.Features;

public static class LeaveServer
{
    public record LeaveServerCommand(Guid ServerId, string UserId) : IRequest<bool>;
    
    public class Query : IRequestHandler<LeaveServerCommand, bool>
    {
        private readonly DoveDbContext _context;

        public Query(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(LeaveServerCommand request, CancellationToken cancellationToken)
        {
            var serverToUpdate = await _context.Servers
                .Where(x => x.Id == request.ServerId)
                .Include(m => m.Members)
                .AsTracking()
                .FirstAsync(cancellationToken);

            var member = await _context.Users
                .AsTracking()
                .FirstAsync(m => m.Id == Guid.Parse(request.UserId), cancellationToken);
            
            if(member is null)
                throw new NotFoundException("User", member);
            
            if (serverToUpdate is null)
                throw new NotFoundException("Server", serverToUpdate);
            
            serverToUpdate.Members.Remove(member);
            
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}