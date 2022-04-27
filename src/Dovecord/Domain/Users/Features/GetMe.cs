using AutoMapper;
using DataAccess.Database;
using Dovecord.Domain.Servers.Dto;
using Dovecord.Domain.Users.Dto;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Users.Features;

public static class GetMe
{
    public record GetMeQuery : IRequest<UserDto>;

    public class QueryHandler : IRequestHandler<GetMeQuery, UserDto>
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

        public async Task<UserDto> Handle(GetMeQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = Guid.Parse(_currentUserService.UserId);
            var filteredServer = await _context.Users
                .Where(user => user.Id == currentUserId)
                .FirstOrDefaultAsync(cancellationToken);
            
            return _mapper.Map<UserDto>(filteredServer);
        }
    }
}