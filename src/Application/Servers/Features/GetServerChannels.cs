using Application.Database;
using Domain.Channels.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class GetServerChannels
{
    public record GetServerChannelsQuery(Guid serverId) : IRequest<List<ChannelDto>>;

    public class QueryHandler : IRequestHandler<GetServerChannelsQuery, List<ChannelDto>>
    {
        private readonly DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChannelDto>> Handle(GetServerChannelsQuery request, CancellationToken cancellationToken)
        {
            var filteredServer = await _context.Servers
                .Where(server => server.Id == request.serverId)
                .Select(servers => servers.Channels
                    .Select(chn => new ChannelDto
                    {
                        Id = chn.Id,
                        Type = chn.Type,
                        Name = chn.Name,
                        Topic = chn.Topic,
                        ServerId = chn.ServerId,
                        //Server = null,
                        //Messages = null,
                        //Recipients = null
                    }).ToList()
                ).FirstOrDefaultAsync(cancellationToken);

            return filteredServer;
        }
    }
}