using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<bool> CreateUserAsync(Guid userId, string username);
    Task<bool> CheckIfUserExistAsync(Guid userId);
    Task<bool> UserLoggedOnAsync(Guid user);
    Task<bool> UserLoggedOffAsync(Guid user);
    Task<User> GetUserByIdAsync(Guid id);
}