using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Dtos.Channel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Channels.Features;

public static class GetChannelList
{
    public record ChannelListQuery : IRequest<List<ChannelDto>>;

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
            var channels = await _context.Channels.ToListAsync(cancellationToken);
            return _mapper.Map<List<ChannelDto>>(channels);
        }
    }
}