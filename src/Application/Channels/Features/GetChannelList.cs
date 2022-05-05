using Application.Database;
using Domain.Channels.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Channels.Features;

public static class GetChannelList
{
    public record ChannelListQuery(Guid serverId) : IRequest<List<ChannelDto>>;

    public class QueryHandler : IRequestHandler<ChannelListQuery, List<ChannelDto>>
    {
        private readonly DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<ChannelDto>> Handle(ChannelListQuery request, CancellationToken cancellationToken)
        {
            var channels = await _context.Servers
                .Where(server => server.Id == request.serverId)
                .Select(channels => channels.Channels.Select(chnl => new ChannelDto
                {
                    Id = chnl.Id,
                    Type = chnl.Type,
                    Name = chnl.Name,
                    Topic = chnl.Topic,
                    ServerId = chnl.ServerId
                }).FirstOrDefault())
                .ToListAsync(cancellationToken);
            //.FirstOrDefaultAsync(cancellationToken);
            return channels;
        }
    }
}