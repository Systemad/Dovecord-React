using Domain.Channels;
using Domain.Messages;
using Domain.Servers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Database;

public class DoveDbContext : DbContext
{
    public DoveDbContext(DbContextOptions<DoveDbContext> options) : base(options)
    {
    }

    public DbSet<Channel> Channels => Set<Channel>();
    public DbSet<ChannelMessage> ChannelMessages => Set<ChannelMessage>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Server> Servers => Set<Server>();
    public DbSet<UserSettings> UserSettings => Set<UserSettings>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        /*
        modelBuilder.Entity<ChannelMessage>(entity =>
        {
            entity.HasOne(d => d.Channel)
                .WithMany(p => p.ChannelMessages)
                .HasForeignKey(k => k.ChannelId);

            entity.HasOne(u => u.User)
                .WithMany(m => m.SentMessages)
                .HasForeignKey(k => k.UserId);
        });
        */
    }
}