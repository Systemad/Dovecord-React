using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Messages.Dto;
using Dovecord.Exceptions;
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
        
        public Handler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ChannelMessageDto> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var message = _mapper.Map<ChannelMessage>(request.MessageToAdd);
            
            var channel = await _context.Channels
                .Where(x => x.Id == request.MessageToAdd.ChannelId)
                .Include(m => m.Messages)
                .AsTracking()
                .FirstAsync(cancellationToken);
            //.SingleOrDefaultAsync(cancellationToken);

            if (channel is null)
                throw new NotFoundException("Channel", channel.Id);
            
            channel.Messages.Add(message);
            
            //if(channel.Type == 0)
            await _context.SaveChangesAsync(cancellationToken);

            return await _context.ChannelMessages
                .ProjectTo<ChannelMessageDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == message.Id, cancellationToken);
        }
    }
}