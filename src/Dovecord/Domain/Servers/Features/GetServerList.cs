using AutoMapper;
using Dovecord.Databases;
using Dovecord.Domain.Servers.Dto;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Servers.Features;

public static class GetServerList
{
    public record ServerListQuery : IRequest<List<ServerDto>>;

    public class QueryHandler : IRequestHandler<ServerListQuery, List<ServerDto>>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public QueryHandler(DoveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        
        public async Task<List<ServerDto>> Handle(ServerListQuery request, CancellationToken cancellationToken)
        {
            var currentUser = Guid.Parse(_currentUserService.UserId);
            
            var servers = await _context.Servers
                //.Include(x => 
                //    x.Members.Select(u => u.Id == currentUser))
                .ToListAsync(cancellationToken);
            
            return _mapper.Map<List<ServerDto>>(servers);
            //return servers;
        }
    }
}