using Dovecord.Domain.Channels;
using Dovecord.Domain.Messages;
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
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }
    
    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }
    private void UpdateAuditFields()
    {
        var now = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<ChannelMessage>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.UserId = Guid.Parse(_currentUserService?.UserId);
                    entry.Entity.CreatedBy = _currentUserService.Username;
                    entry.Entity.CreatedOn = now;
                    entry.Entity.LastModifiedOn = now;
                    entry.Entity.IsEdit = false;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedOn = now;
                    entry.Entity.IsEdit = true;
                    break;
                
                case EntityState.Deleted:
                    // deleted_at
                    break;
            }
        }
        
        foreach (var entry in ChangeTracker.Entries<User>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Id = Guid.Parse(_currentUserService?.UserId);
                    break;
                case EntityState.Deleted:
                    // deleted_at
                    break;
            }
        }
    }
}