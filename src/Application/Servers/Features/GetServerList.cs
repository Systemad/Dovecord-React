using AutoMapper;
using Domain.Servers.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class GetServerList
{
    public record ServerListQuery : IRequest<List<ServerDto>>;

    public class QueryHandler : IRequestHandler<ServerListQuery, List<ServerDto>>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(IDoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<ServerDto>> Handle(ServerListQuery request, CancellationToken cancellationToken)
        {
            var servers = await _context.Servers
                //.Include(x => 
                //    x.Members.Select(u => u.Id == currentUser))
                .ToListAsync(cancellationToken);
            
            return _mapper.Map<List<ServerDto>>(servers);
            //return servers;
        }
    }
}