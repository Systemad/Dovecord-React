using Dovecord.Domain.Channels;
using Dovecord.Domain.Messages;
using Dovecord.Domain.Servers;
using Dovecord.Domain.Users;
using Dovecord.Domain.Users.Dto;
using Dovecord.Extensions.Services;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Databases;

public class DoveDbContext : DbContext
{
    public DoveDbContext(DbContextOptions<DoveDbContext> options) : base(options)
    {
    }

    public DbSet<Channel> Channels { get; set; }
    public DbSet<ChannelMessage> ChannelMessages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Server> Servers { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        /*
        modelBuilder.Entity<Channel>()
            .HasOne(s => s.Server)
            .WithMany(c => c.Channels);

        modelBuilder.Entity<Server>()
            .HasMany(s => s.Members)
            .WithOne();
        
        modelBuilder.Entity<User>()
            .HasMany(s => s.Servers)
            .WithOne();
        */
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