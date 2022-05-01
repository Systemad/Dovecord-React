using AutoMapper;
using Domain.Channels.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Channels.Features;

public static class UpdateChannel
{
    public record UpdateChannelCommand(Guid Id, ChannelManipulationDto NewChannelData) : IRequest<bool>;
    
    public class Query : IRequestHandler<UpdateChannelCommand, bool>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;
        
        public Query(IDoveDbContext context, IMapper mapper)
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
                throw new NotFoundException("Channel", request.Id);
            
            _mapper.Map(request.NewChannelData, channelToUpdate);  
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}