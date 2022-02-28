using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Messages;
using Dovecord.Dtos.Message;
using Dovecord.Dtos.PrivateMessage;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.PrivateMessage.Features;

public static class AddPrivateMessage
{
    public record AddPrivateMessageCommand(PrivateMessageManipulationDto MessageToAdd) : IRequest<PrivateMessageDto>;

    public class Handler : IRequestHandler<AddPrivateMessageCommand, PrivateMessageDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        
        public Handler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<PrivateMessageDto> Handle(AddPrivateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = _mapper.Map<PrivateMessage>(request.MessageToAdd);
            _context.PrivateMessages.Add(message);
            await _context.SaveChangesAsync(cancellationToken);
            // TODO: figure out
            return await _context.PrivateMessages
                .ProjectTo<PrivateMessageDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == message.Id, cancellationToken);
        }
    }
}