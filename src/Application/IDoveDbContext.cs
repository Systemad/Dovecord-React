using Domain.Channels;
using Domain.Messages;
using Domain.Servers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application;

public interface IDoveDbContext
{
    public DbSet<Channel> Channels { get; set; }
    public DbSet<ChannelMessage> ChannelMessages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Server> Servers { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}