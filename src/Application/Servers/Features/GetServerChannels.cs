using AutoMapper;
using Domain.Channels.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class GetServerChannels
{
    public record GetServerChannelsQuery(Guid serverId) : IRequest<List<ChannelDto>>;

    public class QueryHandler : IRequestHandler<GetServerChannelsQuery, List<ChannelDto>>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(IDoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ChannelDto>> Handle(GetServerChannelsQuery request, CancellationToken cancellationToken)
        {
            var filteredServer = await _context.Servers
                .Where(server => server.Id == request.serverId)
                .Select(servers => servers.Channels)
                .FirstOrDefaultAsync(cancellationToken);
            
            return _mapper.Map<List<ChannelDto>>(filteredServer);
        }
    }
}