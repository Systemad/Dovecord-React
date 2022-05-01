using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Features;

public static class DeleteUser
{
    public record DeleteUserCommand(Guid Id) : IRequest<bool>;
    
    public class Handler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IDoveDbContext _context;
        public Handler(IDoveDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (user is null)
                throw new NotFoundException("User", request.Id);

            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}