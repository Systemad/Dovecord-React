using Application.Common.Exceptions;
using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Channels;

public class Edit
{
    public record Model(Guid Id, string Name) : IRequest;
    
    public class QueryHandler : IRequestHandler<Model>
    {
        private DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Model request, CancellationToken cancellationToken)
        {
            var channel = await _context.TextChannels.Where(x => x.Id == request.Id)
                .AsTracking().SingleOrDefaultAsync(cancellationToken);
            
            if (channel is null)
            {
                throw new NotFoundException(nameof(TextChannel), request.Id);
            }

            channel.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}