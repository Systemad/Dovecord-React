using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Database;
using Domain.Servers.Dto;
using Dovecord.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Servers.Features;

public static class GetServerById
{
    public record GetServerByIdGetQuery(Guid Id) : IRequest<ServerDto>;

    public class QueryHandler : IRequestHandler<GetServerByIdGetQuery, ServerDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServerDto> Handle(GetServerByIdGetQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Servers
                .Include(channels => channels.Channels)
                .Include(members => members.Members)
                .ProjectTo<ServerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result is null)
                throw new NotFoundException("Server", request.Id);

            //return _mapper.Map<ServerDto>(result);
            return result;
        }
    }
}