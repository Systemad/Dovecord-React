using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Exceptions;
using Infrastructure.Dtos.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Dovecord.Dtos.Channel;

namespace Dovecord.Domain.Users.Features;

public static class GetUser
{
    public record UserQuery(Guid Id) : IRequest<UserDto>;

    public class QueryHandler : IRequestHandler<UserQuery, UserDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result is null)
                throw new NotFoundException("User", request.Id);

            return result;
        }
    }
}