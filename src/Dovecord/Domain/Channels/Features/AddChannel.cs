using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Channels.Dto;
using Dovecord.Domain.Servers;
using Dovecord.Domain.Servers.Features;
using Dovecord.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Channels.Features;

public static class AddChannel
{
    public record AddChannelCommand(ChannelManipulationDto ChannelToAdd) : IRequest<ChannelDto>;

    public class Handler : IRequestHandler<AddChannelCommand, ChannelDto>
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
        
        public async Task<ChannelDto> Handle(AddChannelCommand request, CancellationToken cancellationToken)
        {
            var serverToUpdate = await _context.Servers
                //.Where(x => x.Id == request.ChannelToAdd.ServerId)
                .Include(m => m.Channels)
                .AsTracking()
                .FirstAsync(cancellationToken);

            if (serverToUpdate is null)
                throw new NotFoundException("Server", request.ChannelToAdd.ServerId);
            
            var channel = _mapper.Map<Channel>(request.ChannelToAdd);
            serverToUpdate.Channels.Add(channel);
            await _context.SaveChangesAsync(cancellationToken);
            var newChannel = await _context.Channels
                .ProjectTo<ChannelDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == channel.Id, cancellationToken);
            
            return newChannel;
        }
    }
}