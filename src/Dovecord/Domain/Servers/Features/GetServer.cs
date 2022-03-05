using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Servers.Dto;
using Dovecord.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Servers.Features;

public static class GetServer
{
    public record GetServerQuery(Guid Id) : IRequest<ServerDto>;

    public class QueryHandler : IRequestHandler<GetServerQuery, ServerDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServerDto> Handle(GetServerQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Servers
                .Include(channels => channels.Channels)
                .ProjectTo<ServerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result is null)
                throw new NotFoundException("Server", request.Id);

            return result;
        }
    }
}