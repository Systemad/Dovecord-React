using AutoMapper;
using Dovecord.Databases;
using Dovecord.Dtos.User;
using Dovecord.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Dovecord.Dtos.Channel;

namespace Dovecord.Domain.Users.Features;

public static class UpdateUser
{
    public record UpdateUserCommand(Guid Id, UserManipulationDto NewChannelData) : IRequest<bool>;
    
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
                throw new NotFoundException("User", request.Id);
            
            _mapper.Map(request.NewChannelData, userToUpdate);  
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}