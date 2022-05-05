using Application.Database;
using Domain.Channels;
using Domain.Channels.Dto;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Channels.Features;

public static class AddUserChannel
{
    public record AddUserChannelCommand(Guid Id, Guid RecipientId, Guid InvokerUserId) : IRequest<ChannelDto>;

    public class Handler : IRequestHandler<AddUserChannelCommand, ChannelDto>
    {
        private readonly DoveDbContext _context;

        public Handler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<ChannelDto> Handle(AddUserChannelCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users.FirstAsync(x => x.Id == request.InvokerUserId, cancellationToken);
            var receiptUser = await _context.Users.FirstAsync(x => x.Id == request.RecipientId, cancellationToken);

            var userList = new List<User>
            {
                currentUser,
                receiptUser
            };

            var newDmChannel = new Channel
            {
               Id = request.Id,
               Type = 1,
               Name = string.Empty,
               Topic = null,
               Messages = null,
               Recipients = userList
            };
           
            _context.Channels.Add(newDmChannel);
            await _context.SaveChangesAsync(cancellationToken);
            var query = await _context.Channels.Where(c => c.Id == request.Id)
                .Select(channel => new ChannelDto
                {
                    Id = channel.Id,
                    Type = channel.Type,
                    Name = channel.Name,
                    Topic = channel.Topic,
                    ServerId = channel.ServerId
                }).SingleOrDefaultAsync(cancellationToken);

            return query;
        }
    }
}