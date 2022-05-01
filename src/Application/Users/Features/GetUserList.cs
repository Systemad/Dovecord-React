using AutoMapper;
using Domain.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Features;

public static class GetUserList
{
    public record UserListQuery : IRequest<List<UserDto>>;

    public class QueryHandler : IRequestHandler<UserListQuery, List<UserDto>>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(IDoveDbContext context, IMapper mapper)
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