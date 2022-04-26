using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Channels.Dto;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Channels.Features;

public static class AddUserChannel
{
    public record AddUserChannelCommand(Guid recipientId) : IRequest<ChannelDto>;

    public class Handler : IRequestHandler<AddUserChannelCommand, ChannelDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public Handler(DoveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        
        public async Task<ChannelDto> Handle(AddUserChannelCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users.FirstAsync(x => x.Id == Guid.Parse(_currentUserService.UserId), cancellationToken);
            var receiptUser = await _context.Users.FirstAsync(x => x.Id == request.recipientId, cancellationToken);

            var userList = new List<Users.User>
            {
                currentUser,
                receiptUser
            };

            var newDmChannel = new Channel
            {
               Id = Guid.NewGuid(),
               Type = 1,
               Name = null,
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