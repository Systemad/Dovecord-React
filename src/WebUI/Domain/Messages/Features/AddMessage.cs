using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Dtos.Message;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;

namespace WebUI.Domain.Messages.Features;

public static class AddMessage
{
    public record AddMessageCommand(MessageManipulationDto ChannelToAdd) : IRequest<ChannelMessageDto>;

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
            var message = _mapper.Map<ChannelMessage>(request.ChannelToAdd);
            _context.ChannelMessages.Add(message);
            await _context.SaveChangesAsync(cancellationToken);

            // TODO: figure out
            return await _context.ChannelMessages
                .ProjectTo<ChannelMessageDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == message.Id, cancellationToken);
        }
    }
}