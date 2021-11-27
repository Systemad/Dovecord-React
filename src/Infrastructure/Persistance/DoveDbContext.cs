using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class DoveDbContext : DbContext
{
    public DoveDbContext(DbContextOptions options) : base(options) { }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Channel> TextChannels { get; set; }
    public virtual DbSet<Message> ChannelMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Guid channelid = Guid.NewGuid();
        Guid messageid = Guid.NewGuid();
        Guid userid = Guid.Parse("ca0f4479-5992-4a00-a3d5-d73ae1daff6f");
            
        modelBuilder.Entity<Channel>().HasData(new Channel
        {
            Id = channelid,
            Name = "General"
        });
            
        modelBuilder.Entity<Channel>().HasData(new Channel
        {
            Id = Guid.NewGuid(),
            Name = "Random",
        });
            
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.Parse("ca0f4479-5992-4a00-a3d5-d73ae1daff6f"),
            Username = "danova",
            Online = false
        });

        modelBuilder.Entity<Message>().HasData(new Message
        {
            Id = messageid,
            Content = "First ever channel message",
            CreatedAt = DateTime.Now,
            IsEdit = false,
            UserId = userid,
            ChannelId = channelid
        });
    }
}