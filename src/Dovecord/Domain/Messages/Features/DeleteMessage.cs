using DataAccess.Database;
using Dovecord.Exceptions;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Messages.Features;

public static class DeleteMessage
{
    public record DeleteMessageCommand(Guid Id) : IRequest<bool>;
    
    public class Handler : IRequestHandler<DeleteMessageCommand, bool>
    {
        private readonly DoveDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public Handler(DoveDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _context.ChannelMessages
                .Include(a => a.Author)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (message is null)
                throw new NotFoundException("Message", request.Id);
            
            if (message.Author.Id != Guid.Parse(_currentUserService.UserId))
                return false;
            
            _context.ChannelMessages.Remove(message);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}