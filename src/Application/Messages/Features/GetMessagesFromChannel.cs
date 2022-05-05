using Application.Database;
using Domain.Messages.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Features;

public static class GetMessagesFromChannel
{
    public record MessageListQuery(Guid Id) : IRequest<List<ChannelMessageDto>>;

    public class QueryHandler : IRequestHandler<MessageListQuery, List<ChannelMessageDto>>
    {
        private readonly DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<ChannelMessageDto>> Handle(MessageListQuery request, CancellationToken cancellationToken)
        {

            var query = await _context.Channels
                .Where(server => server.Id == request.Id)
                .Select(channels => channels.Messages.Select(msg => new ChannelMessageDto
                {
                    Id = msg.Id,
                    CreatedOn = msg.CreatedOn,
                    CreatedBy = msg.CreatedBy,
                    IsEdit = msg.IsEdit,
                    LastModifiedOn = msg.LastModifiedOn,
                    Type = msg.Type,
                    Content = msg.Content,
                    //Author = msg.Author,
                    ChannelId = msg.ChannelId,
                    ServerId = msg.ServerId
                }).FirstOrDefault())
                .ToListAsync(cancellationToken);

            return query;
        }
    }
}