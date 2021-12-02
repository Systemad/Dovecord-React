using Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebUI.Databases;

namespace WebUI.Domain.Messages.Features;

public class Delete
{
    public record DeleteMessageCommand(Guid Id) : IRequest;
    
    public class QueryHandler : IRequestHandler<DeleteMessageCommand>
    {
        private DoveDbContext _context;
        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _context.ChannelMessages.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (message is null)
            {
                throw new NotFoundException("Message", request.Id);
                //throw new RestException(HttpStatusCode.NotFound, new {TextChannel = "not found"});    
            }
            
            _context.ChannelMessages.Remove(message);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}