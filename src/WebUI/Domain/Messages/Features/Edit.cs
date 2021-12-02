using AutoMapper;
using Domain.Channels;
using Infrastructure.Dtos.Message;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;
using Channel = WebUI.Domain.Channels.Channel;
using NotFoundException = WebUI.Exceptions.NotFoundException;

namespace WebUI.Domain.Messages.Features;

public class Edit
{
    public record UpdateChannelCommand(Guid Id, MessageManipulationDto Message) : IRequest;
    
    public class QueryHandler : IRequestHandler<UpdateChannelCommand>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = await _context.ChannelMessages
                .Where(x => x.Id == request.Id)
                .AsTracking()
                .SingleOrDefaultAsync(cancellationToken);
            
            if (channel is null)
            {
                throw new NotFoundException(nameof(Channel), request.Id);
            }

            // TODO: Add this to SaveChanges method?
            channel.IsEdit = true;
            _mapper.Map(request.Message, channel);
            
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}