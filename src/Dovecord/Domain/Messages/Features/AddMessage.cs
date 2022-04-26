using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Messages.Dto;
using Dovecord.Exceptions;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Messages.Features;

public static class AddMessage
{
    public record AddMessageCommand(MessageManipulationDto MessageToAdd) : IRequest<ChannelMessageDto>;

    public class Handler : IRequestHandler<AddMessageCommand, ChannelMessageDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        
        public Handler(DoveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        
        public async Task<ChannelMessageDto> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var currentUserName = _currentUserService.Username;
            var currentUserId = _currentUserService.UserId;
            
            var message = _mapper.Map<ChannelMessage>(request.MessageToAdd);
            message.AuthorId = Guid.Parse(currentUserId);
            message.CreatedBy = currentUserName;
            message.CreatedOn = DateTime.Now;
            
            var channel = await _context.Channels
                .Where(x => x.Id == request.MessageToAdd.ChannelId)
                .FirstAsync(cancellationToken);

            if (channel is null)
                throw new NotFoundException("Channel", channel.Id);
            
            if (message.Type == 0)
            {
                var serverId =
                    await _context.Servers
                        .Where(server => server.Channels != null && server.Channels
                            .Any(ch => ch.Id == request.MessageToAdd.ChannelId)).Select(i => i.Id)
                        .FirstAsync(cancellationToken);
                message.ServerId = serverId;
            }

            await _context.AddAsync(message, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var returnmessage = await _context.ChannelMessages
                .ProjectTo<ChannelMessageDto>(_mapper.ConfigurationProvider)
                .FirstAsync(c => c.Id == message.Id, cancellationToken);

            return returnmessage;
        }
    }
}