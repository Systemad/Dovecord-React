using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Channels.Queries;

public class GetChannelByIdQuery : IRequest<TextChannel>
{
    /*
    public record Query(Guid Id) : IRequest<Response>;

    public record Response(Guid Id, string Name);
    */
    public Guid Id { get; }
    public GetChannelByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetChannelByIdQueryHandler : IRequestHandler<GetChannelByIdQuery, TextChannel>
{
    //private readonly DoveDbContext _context;
    private readonly IMapper _mapper;
    
    public GetChannelByIdQueryHandler(/*DoveDbContext context,*/ IMapper mapper)
    {
        //_context = context;
        _mapper = mapper;
    }

    public async Task<TextChannel> Handle(GetChannelByIdQuery request, CancellationToken cancellationToken)
    {
        //var channel = await _context.TextChannels.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        //return channel == null ? null : new TextChannel(channel.Id, channel.Name);
        var channel1 = new TextChannel
        {
            Id = Guid.Parse("faff15b6-4bee-45c7-8719-1f68b9e49729"),
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
        var hello = channelist.Find(a => a.Id == request.Id);
        return channelist.Find(a => a.Id == request.Id);
        // await _context.TextChannels.ProjectTo<TextChannel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        //throw new NotImplementedException();
    }
}