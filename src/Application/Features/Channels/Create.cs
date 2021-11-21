using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Channels;

public class Create
{
    public record Command(string Name) : IRequest<TextChannel>;

    public class QueryHandler : IRequestHandler<Command, TextChannel>
    {
        private DoveDbContext _context;

        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }
        
        public async Task<TextChannel> Handle(Command request, CancellationToken cancellationToken)
        {
            
            //var channel = await _context.TextChannels.FirstAsync(x => x.Name == request.Name, cancellationToken);
            
            //if (channel is not null)
            //    return new ArgumentNullException();
            var newchannel = new TextChannel
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };
            await _context.AddAsync(newchannel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newchannel;
        }
    }
}