using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Servers;
using Domain.Servers.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class AddServer
{
    public record AddServerCommand(ServerCreatedEvent ServerCreatedEvent) : IRequest<ServerDto>;

    public class Handler : IRequestHandler<AddServerCommand, ServerDto>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IDoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ServerDto> Handle(AddServerCommand request, CancellationToken cancellationToken)
        {
            var mapServer = _mapper.Map<Server>(request.ServerCreatedEvent);
            mapServer.OwnerUserId = request.ServerCreatedEvent.InvokerUserId;
            _context.Servers.Add(mapServer);
            await _context.SaveChangesAsync(cancellationToken);
            
            var serverToUpdate = await _context.Servers
                .Where(x => x.Id == mapServer.Id)
                .Include(m => m.Members)
                .AsTracking()
                .FirstAsync(cancellationToken);

            var member = await _context.Users
                .AsTracking()
                .FirstAsync(m => m.Id == request.ServerCreatedEvent.InvokerUserId, cancellationToken);
            
            serverToUpdate.Members.Add(member);
            await _context.SaveChangesAsync(cancellationToken);
            
            var newServer = await _context.Servers
                .ProjectTo<ServerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == mapServer.Id, cancellationToken);

            return newServer;


        }
    }
}