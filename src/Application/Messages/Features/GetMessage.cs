using Application.Database;
using Domain.Messages.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Features;

public static class GetMessage
{
    public record MessageQuery(Guid Id) : IRequest<ChannelMessageDto>;

    public class QueryHandler : IRequestHandler<MessageQuery, ChannelMessageDto>
    {
        private readonly DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<ChannelMessageDto> Handle(MessageQuery request, CancellationToken cancellationToken)
        {
            var query = await _context.ChannelMessages.Where(m => m.Id == request.Id)
                .Select(msg => new ChannelMessageDto
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
                }).FirstOrDefaultAsync(cancellationToken);
            
            return query;
        }
    }
}