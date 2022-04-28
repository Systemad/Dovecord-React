using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Database;
using Domain.Users.Dto;
using Dovecord.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Users.Features;

public static class UpdateUserSettings
{
    public record UpdateUserSettingsCommand(Guid UserId, UserSettingsManipulationDto NewUserData) : IRequest<UserSettingsDto>;
    
    public class Query : IRequestHandler<UpdateUserSettingsCommand, UserSettingsDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        
        public Query(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserSettingsDto> Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _context.UserSettings
                .Where(x => x.Id == request.UserId)
                .AsTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (userToUpdate is null)
                //return false;
                throw new NotFoundException("User", request.UserId);
            
            _mapper.Map(request.NewUserData, userToUpdate);  
            await _context.SaveChangesAsync(cancellationToken);

            var result = await _context.UserSettings
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

            return _mapper.Map<UserSettingsDto>(result);
        }
    }
}