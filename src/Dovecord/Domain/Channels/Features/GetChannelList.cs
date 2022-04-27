using AutoMapper;
using DataAccess.Database;
using Dovecord.Domain.Channels.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Channels.Features;

public static class GetChannelList
{
    public record ChannelListQuery(Guid serverId) : IRequest<List<ChannelDto>>;

    public class QueryHandler : IRequestHandler<ChannelListQuery, List<ChannelDto>>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<ChannelDto>> Handle(ChannelListQuery request, CancellationToken cancellationToken)
        {
            var channels = await _context.Servers
                .Where(server => server.Id == request.serverId)
                .Select(channels => channels.Channels)
                .FirstOrDefaultAsync(cancellationToken);
            
            return _mapper.Map<List<ChannelDto>>(channels);
        }
    }
}