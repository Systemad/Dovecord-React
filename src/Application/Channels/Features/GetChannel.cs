using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Channels.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Channels.Features;

public static class GetChannel
{
    public record ChannelQuery(Guid Id) : IRequest<ChannelDto>;

    public class QueryHandler : IRequestHandler<ChannelQuery, ChannelDto>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(IDoveDbContext context, IMapper mapper)
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