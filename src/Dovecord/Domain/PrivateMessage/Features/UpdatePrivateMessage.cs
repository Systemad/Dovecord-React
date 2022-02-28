using AutoMapper;
using Dovecord.Databases;
using Dovecord.Exceptions;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.PrivateMessage.Features;

public static class UpdatePrivateMessage
{
    public record UpdatePrivateMessageCommand(Guid Id, string NewMessageData) : IRequest<bool>;
    
    public class Query : IRequestHandler<UpdatePrivateMessageCommand, bool>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        
        public Query(DoveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdatePrivateMessageCommand request, CancellationToken cancellationToken)
        {
            var messageToUpdate = await _context.PrivateMessages
                .Where(x => x.Id == request.Id)
                .AsTracking()
                .SingleOrDefaultAsync(cancellationToken);
            
            if (messageToUpdate is null)
                throw new NotFoundException("Message", request.Id);
            
            if (Guid.Parse(_currentUserService.UserId) != messageToUpdate.UserId)
                return false;
            
            _mapper.Map(request.NewMessageData, messageToUpdate);  
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}