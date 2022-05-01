using AutoMapper;
using Domain.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Features;

public static class GetMe
{
    public record GetMeQuery(Guid UserId) : IRequest<UserDto>;

    public class QueryHandler : IRequestHandler<GetMeQuery, UserDto>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(IDoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetMeQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = request.UserId;
            var filteredServer = await _context.Users
                .Where(user => user.Id == currentUserId)
                .FirstOrDefaultAsync(cancellationToken);
            
            return _mapper.Map<UserDto>(filteredServer);
        }
    }
}