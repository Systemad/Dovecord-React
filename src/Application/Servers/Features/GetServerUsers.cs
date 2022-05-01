using AutoMapper;
using Domain.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class GetServerUsers
{
    public record GetServerUsersQuery(Guid serverId) : IRequest<List<UserDto>>;

    public class QueryHandler : IRequestHandler<GetServerUsersQuery, List<UserDto>>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(IDoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetServerUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Servers
                .Where(server => server.Id == request.serverId)
                .Select(servers => servers.Members)
                .FirstOrDefaultAsync(cancellationToken);
            var mappedUsers = _mapper.Map<List<UserDto>>(users);
            return mappedUsers;
        }
    }
}