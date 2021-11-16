using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Channels.Queries;

public class GetChannelByIdQuery : IRequest<TextChannel>
{
    public Guid Id { get; }
    public GetChannelByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetChannelByIdQueryHandler : IRequestHandler<GetChannelByIdQuery, TextChannel>
{
    private readonly DoveDbContext _context;
    
    public GetChannelByIdQueryHandler(DoveDbContext context)
    {
        _context = context;
    }

    public async Task<TextChannel> Handle(GetChannelByIdQuery request, CancellationToken cancellationToken)
    {
        var channel = await _context.TextChannels.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        return channel;
        //throw new NotImplementedException();
    }
}