using AutoMapper;
using Domain.Servers.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class UpdateServer
{
    public record UpdateServerCommand(Guid Id, CreateServerModel NewCreateServerData) : IRequest<bool>;
    
    public class Query : IRequestHandler<UpdateServerCommand, bool>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;
        
        public Query(IDoveDbContext context, IMapper mapper)
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