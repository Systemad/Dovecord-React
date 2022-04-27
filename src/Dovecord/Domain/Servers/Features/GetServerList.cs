using AutoMapper;
using DataAccess.Database;
using Dovecord.Domain.Servers.Dto;
using Dovecord.Domain.Users.Dto;
using Dovecord.Domain.Users.Features;
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
        private readonly IMediator _mediator;

        public QueryHandler(DoveDbContext context, IMapper mapper, ICurrentUserService currentUserService, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _mediator = mediator;
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