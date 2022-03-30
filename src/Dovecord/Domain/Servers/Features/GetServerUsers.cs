using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Channels.Dto;
using Dovecord.Domain.Servers.Dto;
using Dovecord.Domain.Users.Dto;
using Dovecord.Domain.Users.Features;
using Dovecord.Exceptions;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Servers.Features;

public static class GetServerUsers
{
    public record GetServerUsersQuery(Guid serverId) : IRequest<List<UserDto>>;

    public class QueryHandler : IRequestHandler<GetServerUsersQuery, List<UserDto>>
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

        public async Task<List<UserDto>> Handle(GetServerUsersQuery request, CancellationToken cancellationToken)
        {
            var filteredServer = await _context.Servers
                .Where(server => server.Id == request.serverId)
                .Select(servers => servers.Members)
                .FirstOrDefaultAsync(cancellationToken);
            
            return _mapper.Map<List<UserDto>>(filteredServer);
        }
    }
}