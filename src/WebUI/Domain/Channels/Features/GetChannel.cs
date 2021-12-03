using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Dtos.Channel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;
using WebUI.Exceptions;

namespace WebUI.Domain.Channels.Features;

public static class GetChannel
{
    public record ChannelQuery(Guid Id) : IRequest<ChannelDto>;

    public class QueryHandler : IRequestHandler<ChannelQuery, ChannelDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ChannelDto> Handle(ChannelQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Channels
                .ProjectTo<ChannelDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result is null)
                throw new NotFoundException("Channel", request.Id);

            return result;
        }
    }
}