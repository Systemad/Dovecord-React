using AutoMapper;
using Dovecord.Databases;
using Dovecord.Domain.Servers.Dto;
using Dovecord.Domain.Users.Dto;
using Dovecord.Domain.Users.Features;
using Dovecord.Exceptions;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Servers.Features;

public static class JoinServer
{
    public record JoinServerCommand(Guid ServerId) : IRequest<bool>;
    
    public class Query : IRequestHandler<JoinServerCommand, bool>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;
        
        public Query(DoveDbContext context, IMapper mapper, ICurrentUserService currentUserService, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task<bool> Handle(JoinServerCommand request, CancellationToken cancellationToken)
        {
            var updateUser = new UpdateUser.UpdateUserCommand(
                Guid.Parse(_currentUserService.UserId),
                new UserManipulationDto { IsOnline = true });
            var userExist = await _mediator.Send(updateUser);
            if (!userExist)
            {
                var addUser = new AddUser.AddUserCommand(new UserCreationDto
                {
                    IsOnline = true
                });
                await _mediator.Send(addUser);
            }
            
            var serverToUpdate = await _context.Servers
                .Where(x => x.Id == request.ServerId)
                .Include(m => m.Members)
                .AsTracking()
                .FirstAsync(cancellationToken);
                //.SingleOrDefaultAsync(cancellationToken);
                
            var member = await _context.Users
                .AsTracking()
                .FirstAsync(m => m.Id == Guid.Parse(_currentUserService.UserId), cancellationToken);
            
            if (serverToUpdate is null)
                throw new NotFoundException("Server", member);
            
            serverToUpdate.Members.Add(member);
            
            // Mapping not needed
            //_mapper.Map(request.NewServerData, serverToUpdate);  
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}