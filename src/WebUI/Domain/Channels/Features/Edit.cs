using Application.Common.Exceptions;
using AutoMapper;
using Infrastructure.Dtos.Channel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;

namespace WebUI.Domain.Channels.Features;

public class Edit
{
    public record UpdateChannelCommand(Guid Id, ChannelManipulationDto ChannelToUpdate) : IRequest;
    
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
            var channel = await _context.Channels
                .Where(x => x.Id == request.Id)
                .AsTracking()
                .SingleOrDefaultAsync(cancellationToken);
            
            if (channel is null)
            {
                throw new NotFoundException("Channel", request.Id);
            }

            // TODO: Fix entity LastModified etc
            _mapper.Map(request.ChannelToUpdate, channel);  
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}