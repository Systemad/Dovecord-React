using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Messages;
using Domain.Messages.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Features;

public static class AddMessage
{
    public record AddMessageCommand(MessageManipulationDto MessageToAdd) : IRequest<ChannelMessageDto>;

    public class Handler : IRequestHandler<AddMessageCommand, ChannelMessageDto>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;
        
        public Handler(IDoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ChannelMessageDto> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {

            var message = _mapper.Map<ChannelMessage>(request.MessageToAdd);
            message.AuthorId = request.MessageToAdd.UserId;
            message.CreatedBy = request.MessageToAdd.Username;
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

            await _context.ChannelMessages.AddAsync(message, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var returnmessage = await _context.ChannelMessages
                .ProjectTo<ChannelMessageDto>(_mapper.ConfigurationProvider)
                .FirstAsync(c => c.Id == message.Id, cancellationToken);

            return returnmessage;
        }
    }
}