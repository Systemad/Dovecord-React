using Application.Common.Exceptions;
using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Messages;

public class Delete
{
    public record Command(Guid Id) : IRequest;
    
    public class QueryHandler : IRequestHandler<Command>
    {
        private DoveDbContext _context;
        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var message = await _context.ChannelMessages.FirstOrDefaultAsync(x => x.ChannelMessageId == request.Id, cancellationToken);

            if (message is null)
            {
                throw new NotFoundException(nameof(Channel), request.Id);
                //throw new RestException(HttpStatusCode.NotFound, new {TextChannel = "not found"});    
            }
            
            _context.ChannelMessages.Remove(message);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}