using Application.Database;
using Domain.Channels;
using Domain.Channels.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Channels.Features;

// CreateServerCommand
public static class AddServerChannel
{
    public record AddServerChannelCommand(Channel Channel) : IRequest<ChannelDto>;

    public class Handler : IRequestHandler<AddServerChannelCommand, ChannelDto>
    {
        private readonly DoveDbContext _context;
        
        public Handler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<ChannelDto> Handle(AddServerChannelCommand request, CancellationToken cancellationToken)
        {
            var serverToUpdate = await _context.Servers
                .Where(x => x.Id == request.Channel.ServerId)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (serverToUpdate is null)
                throw new NotFoundException("Server", request.Channel);
            
            _context.Channels.Add(request.Channel);
            await _context.SaveChangesAsync(cancellationToken);

            var query = await _context.Channels
                .Where(x => x.Id == request.Channel.Id)
                .Select(chn => new ChannelDto
                {
                    Id = chn.Id,
                    Type = chn.Type,
                    Name = chn.Name,
                    Topic = chn.Topic,
                    ServerId = chn.ServerId
                }).FirstOrDefaultAsync(cancellationToken);

            return query;
        }
    }
}