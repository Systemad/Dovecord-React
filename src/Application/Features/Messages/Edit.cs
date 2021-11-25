using Application.Common.Exceptions;
using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Messages;

public class Edit
{
    public record Model(Guid Id, ChannelMessage Message) : IRequest;
    
    public class QueryHandler : IRequestHandler<Model>
    {
        private DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Model request, CancellationToken cancellationToken)
        {
            var channel = await _context.ChannelMessages.Where(x => x.Id == request.Id)
                .AsTracking().SingleOrDefaultAsync(cancellationToken);
            
            if (channel is null)
            {
                throw new NotFoundException(nameof(TextChannel), request.Id);
            }

            channel.Content = request.Message.Content;
            channel.IsEdit = true;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}