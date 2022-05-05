using Application.Database;
using Domain.Messages;
using Domain.Messages.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Features;

public static class AddMessage
{
    public record AddMessageCommand(Guid Id, Guid UserId, string Username, string Content, int Type, Guid ChannelId, Guid ServerId)
        : IRequest<ChannelMessageDto>;

    public class Handler : IRequestHandler<AddMessageCommand, ChannelMessageDto>
    {
        private readonly DoveDbContext _context;
        
        public Handler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<ChannelMessageDto> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {

            var message = new ChannelMessage
            {
                Id = request.Id,
                Content = request.Content,
                CreatedBy = request.Username,
                CreatedOn = DateTime.Now,
                IsEdit = false,
                LastModifiedOn = null,
                Type = request.Type,
                ChannelId = request.ChannelId,
                ServerId = request.ServerId,
                AuthorId = request.UserId,
            };
            
            if (message.Type == 0)
            {
                var serverId =
                    await _context.Servers
                        .Where(server => server.Channels != null && server.Channels
                            .Any(ch => ch.Id == request.ChannelId)).Select(i => i.Id)
                        .FirstAsync(cancellationToken);
                message.ServerId = serverId;
            }

            await _context.ChannelMessages.AddAsync(message, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

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