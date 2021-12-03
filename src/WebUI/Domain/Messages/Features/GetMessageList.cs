using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Dtos.Channel;
using Infrastructure.Dtos.Message;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;
using WebUI.Domain.Channels;

namespace WebUI.Domain.Messages.Features;

public static class GetMessageList
{
    public record MessageListQuery : IRequest<List<ChannelMessageDto>>;

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
            var channels = await _context.Channels.ToListAsync(cancellationToken);
            var collection = _context as IQueryable<ChannelMessageDto>;

            var dtoCollection = collection.ProjectTo<ChannelDto>(_mapper.ConfigurationProvider);
            
            // TODO: Check if correct
            return _mapper.Map<List<ChannelMessageDto>>(channels);
        }
    }
}