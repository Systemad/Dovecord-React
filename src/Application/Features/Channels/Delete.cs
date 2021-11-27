using System.Net;
using Application.Common.Exceptions;
using Domain.Entities;
using Infrastructure.Errors;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Channels;

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
            var channel = await _context.TextChannels.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (channel is null)
            {
                throw new NotFoundException(nameof(Channel), request.Id);
                //throw new RestException(HttpStatusCode.NotFound, new {TextChannel = "not found"});    
            }
            
            _context.TextChannels.Remove(channel);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}