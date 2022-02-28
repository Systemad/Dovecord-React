using AutoMapper;
using Dovecord.Databases;
using Dovecord.Dtos.Message;
using Dovecord.Dtos.PrivateMessage;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.PrivateMessage.Features;

public static class GetPrivateMessageList
{
    public record PrivateMessageListQuery(Guid contactId) : IRequest<List<PrivateMessageDto>>;

    public class QueryHandler : IRequestHandler<PrivateMessageListQuery, List<PrivateMessageDto>>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public QueryHandler(DoveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        
        public async Task<List<PrivateMessageDto>> Handle(PrivateMessageListQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;
            var pmessages = await _context.PrivateMessages.Where(c =>
                c.UserId == request.contactId && c.ReceiverUserId == Guid.Parse(currentUserId)
                ||
                c.UserId == Guid.Parse(currentUserId) && c.ReceiverUserId == request.contactId)
                .ToListAsync(cancellationToken: cancellationToken);
            
            //var messages = await _context.PrivateMessages
            //    .Where(m => m.ReceiverUserId == request.contactId).ToListAsync(cancellationToken: cancellationToken);
            
            
            return _mapper.Map<List<PrivateMessageDto>>(pmessages);
        }
    }
}