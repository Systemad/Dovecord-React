using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Channels;
using Infrastructure.Database;
using Domain.Channels.Dto;
using Domain.Users;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Channels.Features;

public static class AddUserChannel
{
    public record AddUserChannelCommand(Guid recipientId, Guid InvokerUserId) : IRequest<ChannelDto>;

    public class Handler : IRequestHandler<AddUserChannelCommand, ChannelDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public Handler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ChannelDto> Handle(AddUserChannelCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users.FirstAsync(x => x.Id == request.InvokerUserId, cancellationToken);
            var receiptUser = await _context.Users.FirstAsync(x => x.Id == request.recipientId, cancellationToken);

            var userList = new List<User>
            {
                currentUser,
                receiptUser
            };

            var newDmChannel = new Channel
            {
               Id = Guid.NewGuid(),
               Type = 1,
               Name = string.Empty,
               Topic = null,
               Messages = null,
               Recipients = userList
            };
           
            _context.Channels.Add(newDmChannel);
            await _context.SaveChangesAsync(cancellationToken);
            var newChannel = await _context.Channels
                .ProjectTo<ChannelDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == newDmChannel.Id, cancellationToken);
            
            return newChannel;
        }
    }
}