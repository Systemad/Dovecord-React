using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Channels;
using Domain.Channels.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Channels.Features;

public static class AddServerChannel
{
    public record AddServerChannelCommand(ChannelManipulationDto ChannelToAdd, Guid serverId) : IRequest<ChannelDto>;

    public class Handler : IRequestHandler<AddServerChannelCommand, ChannelDto>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public Handler(IDoveDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }
        
        public async Task<ChannelDto> Handle(AddServerChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = _mapper.Map<Channel>(request.ChannelToAdd);
            channel.ServerId = request.serverId;
            
            var serverToUpdate = await _context.Servers
                .Where(x => x.Id == channel.ServerId)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (serverToUpdate is null)
                throw new NotFoundException("Server", request.serverId);
            
            _context.Channels.Add(channel);
            await _context.SaveChangesAsync(cancellationToken);
            
            var newChannel = await _context.Channels
                .ProjectTo<ChannelDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == channel.Id, cancellationToken);
            return newChannel;
        }
    }
}