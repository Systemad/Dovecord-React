using Microsoft.EntityFrameworkCore;
using WebUI.Domain.Channels;
using WebUI.Domain.Messages;
using WebUI.Domain.Users;
using WebUI.Services;

namespace WebUI.Databases;

public class DoveDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;
    public DoveDbContext(DbContextOptions<DoveDbContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    public virtual DbSet<Channel> Channels { get; set; }
    public virtual DbSet<ChannelMessage> ChannelMessages { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChannelMessage>(entity =>
        {
            entity.HasOne(d => d.Channel)
                .WithMany(p => p.ChannelMessages)
                .HasForeignKey(k => k.ChannelId);

            entity.HasOne(u => u.User)
                .WithMany(m => m.SentMessages)
                .HasForeignKey(k => k.UserId);
        });
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
        foreach (var entry in ChangeTracker.Entries<BaseMessageEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = Guid.Parse(_currentUserService?.UserId);;
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
    }
}