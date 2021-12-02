using Application.Interfaces;
using Domain.Entities;
using Domain.Users;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService : IUserService
{
    private DoveDbContext _context;

    public UserService(DoveDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<bool> CreateUserAsync(Guid userId, string username)
    {
        var newUser = new User
        {
            UserId = userId,
            Name = username,
            IsOnline = true
        };
            
        await _context.Users.AddAsync(newUser);
        var created = await _context.SaveChangesAsync();
        return created > 0;
    }

    public async Task<bool> CheckIfUserExistAsync(Guid userId)
    {
        var userexist = await _context.Users.AnyAsync(u => u.UserId == userId);
        return userexist;
    }

    public async Task<bool> UserLoggedOnAsync(Guid userId)
    {
        var user = await _context.Users.Where(x => x.UserId == userId).AsTracking().SingleOrDefaultAsync();
        //_context.Attach(user);
        user.IsOnline = true;
        //_context.Update(user);
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

    public async Task<bool> UserLoggedOffAsync(Guid userId)
    {
        var user = await _context.Users.Where(x => x.UserId == userId).AsTracking().SingleOrDefaultAsync();
        //_context.Attach(user);
        user.IsOnline = false;
        //_context.Update(user);
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.UserId == id);
    }
}