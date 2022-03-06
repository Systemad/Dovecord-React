using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Channels.Dto;
using Dovecord.Domain.Servers;
using Dovecord.Domain.Servers.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Channels.Features;

public static class AddChannel
{
    public record AddChannelCommand(ChannelManipulationDto ChannelToAdd) : IRequest<Channel>;

    public class Handler : IRequestHandler<AddChannelCommand, Channel>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public Handler(DoveDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }
        
        public async Task<Channel> Handle(AddChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = _mapper.Map<Channel>(request.ChannelToAdd);
            
            var serverToUpdate = await _context.Servers
                .Where(x => x.Id == request.ChannelToAdd.ServerId)
                .Include(m => m.Channels)
                .AsTracking()
                .FirstAsync(cancellationToken);
            
            serverToUpdate.Channels.Add(channel);
            
            // Mapping not needed
            //_mapper.Map(request.NewServerData, serverToUpdate);  
            await _context.SaveChangesAsync(cancellationToken);
            
            // TODO : Fix
            var newChannel = await _context.Channels
                .FirstOrDefaultAsync(c => c.Id == channel.Id, cancellationToken);
            
            return newChannel;
        }
    }
}