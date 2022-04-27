using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess.Database;
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
            var filteredServer = await _context.Servers
                .Where(server => server.Id == request.serverId)
                .Select(servers => servers.Channels)
                .FirstOrDefaultAsync(cancellationToken);
            
            return _mapper.Map<List<ChannelDto>>(filteredServer);
        }
    }
}