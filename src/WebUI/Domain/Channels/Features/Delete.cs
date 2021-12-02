using Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;

namespace WebUI.Domain.Channels.Features;

public class Delete
{
    public record DeleteChannelCommand(Guid Id) : IRequest;
    
    public class QueryHandler : IRequestHandler<DeleteChannelCommand>
    {
        private DoveDbContext _context;
        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = await _context.Channels
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (channel is null)
            {
                throw new NotFoundException("Channel", request.Id);
                //throw new RestException(HttpStatusCode.NotFound, new {TextChannel = "not found"});    
            }
            
            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}