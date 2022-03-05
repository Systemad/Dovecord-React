using AutoMapper;
using Dovecord.Databases;
using Dovecord.Domain.Users.Dto;
using Dovecord.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Users.Features;

public static class UpdateUser
{
    public record UpdateUserCommand(Guid Id, UserManipulationDto NewUserData) : IRequest<bool>;
    
    public class Query : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        
        public Query(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _context.Users
                .Where(x => x.Id == request.Id)
                .AsTracking()
                .SingleOrDefaultAsync(cancellationToken);

            if (userToUpdate is null)
                return false;
                //throw new NotFoundException("User", request.Id);
            
            _mapper.Map(request.NewUserData, userToUpdate);  
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}