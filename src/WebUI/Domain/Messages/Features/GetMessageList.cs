using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;
using WebUI.Domain.Channels;
using WebUI.Dtos.Channel;
using WebUI.Dtos.Message;

namespace WebUI.Domain.Messages.Features;

public static class GetMessageList
{
    public record MessageListQuery(Guid id) : IRequest<List<ChannelMessageDto>>;

    public class QueryHandler : IRequestHandler<MessageListQuery, List<ChannelMessageDto>>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<ChannelMessageDto>> Handle(MessageListQuery request, CancellationToken cancellationToken)
        {
            var messages = await _context.ChannelMessages
                .Where(m => m.ChannelId == request.id)
                .ToListAsync(cancellationToken);

            // TODO TODO
            var chcch = await _context.Channels
                .Where(i => i.Id == request.id)
                .Select(x => x.ChannelMessages)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            
            var collection = new List<ChannelMessage>().AsQueryable();

            var dtoCollection = collection.ProjectTo<ChannelDto>(_mapper.ConfigurationProvider);
            
            // TODO: Check if correct
            return _mapper.Map<List<ChannelMessageDto>>(dtoCollection);
        }
    }
}