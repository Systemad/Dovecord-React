using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Features;

public static class DoesUserExist
{
    public record DoesUserExistCommand(Guid UserId) : IRequest<bool>;

    public class QueryHandler : IRequestHandler<DoesUserExistCommand, bool>
    {
        private readonly IDoveDbContext _context;
        private readonly IMediator _mediator;

        public QueryHandler(IDoveDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DoesUserExistCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _context.Users.AnyAsync(u => u.Id == request.UserId, cancellationToken);
            return userExist;
        }
    }
}