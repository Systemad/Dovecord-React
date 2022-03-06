using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Channels;
using Dovecord.Domain.Servers.Dto;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Servers.Features;

public static class AddServer
{
    public record AddServerCommand(ServerManipulationDto ServerToAdd) : IRequest<Server>;

    public class Handler : IRequestHandler<AddServerCommand, Server>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        
        public Handler(DoveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        
        public async Task<Server> Handle(AddServerCommand request, CancellationToken cancellationToken)
        {
            var newSever = _mapper.Map<Server>(request.ServerToAdd);
            newSever.OwnerUserId = Guid.Parse(_currentUserService.UserId);
            _context.Servers.Add(newSever);
            await _context.SaveChangesAsync(cancellationToken);
            
            return await _context.Servers
                .FirstOrDefaultAsync(c => c.Id == newSever.Id, cancellationToken);
        }
    }
}