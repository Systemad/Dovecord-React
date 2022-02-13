using AutoMapper;
using Dovecord.Databases;
using Dovecord.Dtos.Channel;
using Dovecord.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Channels.Features;

public static class UpdateChannel
{
    public record UpdateChannelCommand(Guid Id, ChannelManipulationDto NewChannelData) : IRequest<bool>;
    
    public class Query : IRequestHandler<UpdateChannelCommand, bool>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        
        public Query(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateChannelCommand request, CancellationToken cancellationToken)
        {
            var channelToUpdate = await _context.Channels
                .Where(x => x.Id == request.Id)
                .AsTracking()
                .SingleOrDefaultAsync(cancellationToken);

            if (channelToUpdate is null)
                return false;
                //throw new NotFoundException("Channel", request.Id);
            
            _mapper.Map(request.NewChannelData, channelToUpdate);  
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}