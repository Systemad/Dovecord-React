using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Messages;

public class Details
{

    public record Channel(TextChannel TextChannel);
    public record Query(Guid Id) : IRequest<Channel>;

    public class QueryHandler : IRequestHandler<Query, Channel>
    {
        private readonly DoveDbContext _context;
        //private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context /*, IMapper mapper*/)
        {
            _context = context;
            //_mapper = mapper;
        }

        public async Task<Channel> Handle(Query request, CancellationToken cancellationToken)
        {
            var channel = await _context.TextChannels
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            return new Channel(channel);
            /*
            return channel == null ? null : new TextChannel
            {
                Id = request.ChannelId,
                Name = request.;
            };
            */
            //return _context.TextChannels.FindAsync(a => a.Id == request.Id);
        }
    }
}