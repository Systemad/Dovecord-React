using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Dovecord.Domain.Channels;
using Dovecord.Domain.Users.Dto;
using Dovecord.Extensions.Services;

namespace Dovecord.Domain.Users.Features;

public static class AddUser
{
    public record AddUserCommand(UserCreationDto UserToAdd) : IRequest<UserDto>;

    public class Handler : IRequestHandler<AddUserCommand, UserDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        private ICurrentUserService _currentUserService;

        public Handler(DoveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        
        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.UserToAdd);
            user.Id = Guid.Parse(_currentUserService.UserId);
            user.Username = _currentUserService.Username;
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);
        }
    }
}