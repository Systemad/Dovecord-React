using Application.Database;
using Domain.Messages;
using Domain.Messages.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Features;

public static class AddMessage
{
    public record AddMessageCommandM(ChannelMessage Message)
        : IRequest<ChannelMessageDto>;

    public class Handler : IRequestHandler<AddMessageCommandM, ChannelMessageDto>
    {
        private readonly DoveDbContext _context;
        
        public Handler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<ChannelMessageDto> Handle(AddMessageCommandM request, CancellationToken cancellationToken)
        {
            if (request.Message.Type == 0)
            {
                var serverId =
                    await _context.Servers
                        .Where(server => server.Channels != null && server.Channels
                            .Any(ch => ch.Id == request.Message.ChannelId)).Select(i => i.Id)
                        .FirstAsync(cancellationToken);
                request.Message.ServerId = serverId;
            }

            await _context.ChannelMessages.AddAsync(request.Message, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var query = await _context.ChannelMessages.Where(m => m.Id == request.Message.Id)
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