using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Messages.Dto;
using Dovecord.Exceptions;
using Dovecord.Extensions.Services;
using FluentValidation;
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
            var message = _mapper.Map<ChannelMessage>(request.MessageToAdd);
            message.AuthorId = Guid.Parse(_currentUserService.UserId);
            message.CreatedBy = _currentUserService.Username;
            message.CreatedOn = DateTime.Now;
            
            var channel = await _context.Channels
                //.Where(x => x.Id == request.MessageToAdd.ChannelId)
                .Include(m => m.Messages)
                .AsTracking()
                .FirstAsync(cancellationToken);
            //.SingleOrDefaultAsync(cancellationToken);
            
            if (message.Type == 0)
            {
                var servee =
                    await _context.Servers
                        .Where(server => server.Channels
                            .Any(channel => channel.Id == request.MessageToAdd.ChannelId)).FirstAsync();
                if(servee is not null)
                    message.ServerId = servee.Id;
            }

            if (channel is null)
                throw new NotFoundException("Channel", channel.Id);
            
            channel.Messages.Add(message);
            
            await _context.SaveChangesAsync(cancellationToken);

            var returnmessage = await _context.ChannelMessages
                .ProjectTo<ChannelMessageDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == message.Id, cancellationToken);

            return returnmessage;
        }
    }
}