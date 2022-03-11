using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
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
            
            var updateUser = new UpdateUser.UpdateUserCommand(currentUserId, new UserManipulationDto { IsOnline = true });
            var userExist = await _mediator.Send(updateUser);
            if (!userExist)
            {
                var addUser = new AddUser.AddUserCommand(new UserCreationDto
                {
                    Name = _currentUserService.Username,
                    IsOnline = true
                });
                await _mediator.Send(addUser);
            }
            
            var filteredServer = await _context.Users
                .Where(user => user.Id == currentUserId)
                .Select(servers => servers.Servers)
                .FirstOrDefaultAsync(cancellationToken);
            
            return _mapper.Map<List<ServerDto>>(filteredServer);
        }
    }
}