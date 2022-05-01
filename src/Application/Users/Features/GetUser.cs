using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Features;

public static class GetUser
{
    public record GetUserQuery(Guid Id) : IRequest<UserDto>;

    public class QueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(IDoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result is null)
                //return null;
                throw new NotFoundException("User", request.Id);

            return result;
        }
    }
}