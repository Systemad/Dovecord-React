using Application.Database;
using Domain.Channels.Dto;
using Domain.Servers.Dto;
using Domain.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Servers.Features;

public static class GetServerById
{
    public record GetServerByIdGetQuery(Guid Id) : IRequest<ServerDto>;

    public class QueryHandler : IRequestHandler<GetServerByIdGetQuery, ServerDto>
    {
        private readonly DoveDbContext _context;
        public QueryHandler(DoveDbContext context)
        {
            _context = context;
        }

        public async Task<ServerDto> Handle(GetServerByIdGetQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Servers
                .Where(s => s.Id == request.Id)
                .Select(x => new ServerDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    IconUrl = x.IconUrl,
                    OwnerUserId = x.OwnerUserId
                }).FirstOrDefaultAsync(cancellationToken);
            
            if (result is null)
                throw new NotFoundException("Server", request.Id);
            
            return result;
        }
    }
}