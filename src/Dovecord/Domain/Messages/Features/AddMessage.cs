using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Messages.Dto;
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
                .FirstOrDefaultAsync(c => c.Id == request.MessageToAdd.ChannelId, cancellationToken: cancellationToken);
            
            //if(channel.Type == 0)
                
                
            _context.ChannelMessages.Add(message);
            await _context.SaveChangesAsync(cancellationToken);

            return await _context.ChannelMessages
                .ProjectTo<ChannelMessageDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == message.Id, cancellationToken);
        }
    }
}