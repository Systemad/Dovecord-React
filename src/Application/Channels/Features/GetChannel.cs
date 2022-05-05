using Application.Database;
using Domain.Channels.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Channels.Features;

public static class GetChannel
{
    public record ChannelQuery(Guid Id) : IRequest<ChannelDto>;

    public class QueryHandler : IRequestHandler<ChannelQuery, ChannelDto>
    {
        private readonly DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<ChannelDto> Handle(ChannelQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Channels.Where(c => c.Id == request.Id)
                .Select(chn => new ChannelDto
                {
                    Id = chn.Id,
                    Type = chn.Type,
                    Name = chn.Name,
                    Topic = chn.Topic,
                    ServerId = chn.ServerId
                }).SingleOrDefaultAsync(cancellationToken);

            if (result is null)
                throw new NotFoundException("Channel", request.Id);

            return result;
        }
    }
}