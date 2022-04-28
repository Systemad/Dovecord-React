using AutoMapper;
using Infrastructure.Database;
using Domain.Servers.Dto;
using Dovecord.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Servers.Features;

public static class UpdateServer
{
    public record UpdateServerCommand(Guid Id, CreateServerModel NewCreateServerData) : IRequest<bool>;
    
    public class Query : IRequestHandler<UpdateServerCommand, bool>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        
        public Query(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateServerCommand request, CancellationToken cancellationToken)
        {
            var serverToUpdate = await _context.Servers
                .Where(x => x.Id == request.Id)
                .AsTracking()
                .SingleOrDefaultAsync(cancellationToken);
            
            if (serverToUpdate is null)
                throw new NotFoundException("Server", request.Id);
            
            _mapper.Map(request.NewCreateServerData, serverToUpdate);  
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}