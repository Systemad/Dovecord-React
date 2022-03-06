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

public static class GetServerChannels
{
    public record GetServerChannelsQuery(Guid serverId) : IRequest<List<ChannelDto>>;

    public class QueryHandler : IRequestHandler<GetServerChannelsQuery, List<ChannelDto>>
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

        public async Task<List<ChannelDto>> Handle(GetServerChannelsQuery request, CancellationToken cancellationToken)
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
            
            var filteredServer = await _context.Servers
                .Where(server => server.Id == request.serverId)
                .Select(servers => servers.Channels)
                .FirstOrDefaultAsync(cancellationToken);
            
            return _mapper.Map<List<ChannelDto>>(filteredServer);
        }
    }
}