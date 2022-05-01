using AutoMapper;
using Domain.Servers.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class GetServersOfUser
{
    public record GetServersOfUserQuery(Guid UserId) : IRequest<List<ServerDto>>;

    public class QueryHandler : IRequestHandler<GetServersOfUserQuery, List<ServerDto>>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(IDoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ServerDto>> Handle(GetServersOfUserQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = request.UserId;
            var filteredServer = await _context.Users
                .Where(user => user.Id == currentUserId)
                //.Include(x => x.Servers.Select(x => x.Channels))
                //.Include(x => x.Servers.Select(x => x.Members))
                .Select(servers => servers.Servers)
                .FirstOrDefaultAsync( cancellationToken);
            
            return _mapper.Map<List<ServerDto>>(filteredServer);
        }
    }
}