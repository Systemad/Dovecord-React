using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Dovecord.Domain.Channels;
using Dovecord.Domain.Users.Dto;

namespace Dovecord.Domain.Users.Features;

public static class GetUserList
{
    public record UserListQuery : IRequest<List<UserDto>>;

    public class QueryHandler : IRequestHandler<UserListQuery, List<UserDto>>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<UserDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);
            //return users;
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}