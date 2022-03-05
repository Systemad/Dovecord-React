using Dovecord.Domain.Channels;
using Dovecord.Domain.Messages;
using Dovecord.Domain.Servers;
using Dovecord.Domain.Users;
using Dovecord.Extensions.Services;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Databases;

public class DoveDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;
    public DoveDbContext(DbContextOptions<DoveDbContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    public DbSet<Channel> Channels { get; set; }
    public DbSet<ChannelMessage> ChannelMessages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Server> Servers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        //UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }
    
    public override int SaveChanges()
    {
        //UpdateAuditFields();
        return base.SaveChanges();
    }
}