using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Channels.Queries;

public class GetChannelsQuery : IRequest<List<TextChannel>>
{
    
}

public class GetChannelsQueryHandler : IRequestHandler<GetChannelsQuery, List<TextChannel>>
{
    //private readonly DoveDbContext _context;
    private readonly IMapper _mapper;

    public GetChannelsQueryHandler(/*DoveDbContext context*/ IMapper mapper)
    {
        //_context = context;
        _mapper = mapper;
    }

    public async Task<List<TextChannel>> Handle(GetChannelsQuery request, CancellationToken cancellationToken)
    {
        // TODO: Fix automapper / mapping
        /*
        var channels = await _context.TextChannels.ProjectTo().ToListAsync(cancellationToken: cancellationToken);
            
            .ToListAsync(cancellationToken: cancellationToken);
        */
        var channel1 = new TextChannel
        {
            Id = Guid.NewGuid(),
            Name = "channel2"
        };
        var channel2 = new TextChannel
        {
            Id = Guid.NewGuid(),
            Name = "Chnnel2"
        };
        var channelist = new List<TextChannel>
        {
            channel1,
            channel2
        };

        return channelist;
        //return await _context.TextChannels.ToListAsync(cancellationToken: cancellationToken);
    }
}