using Domain.Channels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;
using Channel = WebUI.Domain.Channels.Channel;

namespace WebUI.Domain.Messages.Features;

public class Details
{

    public record TextChannel(Channel Channel);
    public record Query(Guid Id) : IRequest<TextChannel>;

    public class QueryHandler : IRequestHandler<Query, TextChannel>
    {
        private readonly DoveDbContext _context;
        //private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context /*, IMapper mapper*/)
        {
            _context = context;
            //_mapper = mapper;
        }

        public async Task<TextChannel> Handle(Query request, CancellationToken cancellationToken)
        {
            var channel = await _context.Channels
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            return new TextChannel(channel);
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