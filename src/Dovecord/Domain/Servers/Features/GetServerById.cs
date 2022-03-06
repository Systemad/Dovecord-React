using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Servers.Dto;
using Dovecord.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Servers.Features;

public static class GetServerById
{
    public record GetServerByIdGetQuery(Guid Id) : IRequest<Server>;

    public class QueryHandler : IRequestHandler<GetServerByIdGetQuery, Server>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Server> Handle(GetServerByIdGetQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Servers
                .Include(channels => channels.Channels)
                .Include(members => members.Members)
                //.ProjectTo<ServerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result is null)
                throw new NotFoundException("Server", request.Id);

            //return _mapper.Map<ServerDto>(result);
            return result;
        }
    }
}