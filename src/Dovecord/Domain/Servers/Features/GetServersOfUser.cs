using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess.Database;
using Dovecord.Domain.Servers.Dto;
using Dovecord.Domain.Users.Dto;
using Dovecord.Domain.Users.Features;
using Dovecord.Exceptions;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Servers.Features;

public static class GetServersOfUser
{
    public record GetServersOfUserQuery : IRequest<List<ServerDto>>;

    public class QueryHandler : IRequestHandler<GetServersOfUserQuery, List<ServerDto>>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public QueryHandler(DoveDbContext context, IMapper mapper, ICurrentUserService currentUserService, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task<List<ServerDto>> Handle(GetServersOfUserQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = Guid.Parse(_currentUserService.UserId);
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