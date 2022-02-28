using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Dtos.Message;
using Dovecord.Dtos.PrivateMessage;
using Dovecord.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.PrivateMessage.Features;

public static class GetPrivateMessage
{
    public record PrivateMessageQuery(Guid Id) : IRequest<PrivateMessageDto>;

    public class QueryHandler : IRequestHandler<PrivateMessageQuery, PrivateMessageDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PrivateMessageDto> Handle(PrivateMessageQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.PrivateMessages
                .ProjectTo<PrivateMessageDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result is null)
                throw new NotFoundException("Message", request.Id);

            return result;
        }
    }
}