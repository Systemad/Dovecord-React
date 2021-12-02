using Domain.Channels;
using Domain.Entities;
using Domain.Messages;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class DoveDbContext : DbContext
    {
        public DoveDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<ChannelMessage> ChannelMessages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=dovetest;Username=postgres;Password=Compaq2009");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Channel>(entity =>
            {
                entity.Property(e => e.ChannelId).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(25);

                entity.HasMany(m => m.ChannelMessages).WithOne();
            });
            */
            modelBuilder.Entity<ChannelMessage>(entity =>
            {
                //entity.HasKey(e => e.MessageId)
                //    .HasName("ChannelMessages_pkey");

                //entity.Property(e => e.MessageId).ValueGeneratedNever();

                //entity.Property(e => e.Content).HasColumnType("character varying");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.ChannelMessages)
                    .HasForeignKey(k => k.ChannelId);

                entity.HasOne(u => u.User)
                    .WithMany(m => m.SentMessages)
                    .HasForeignKey(k => k.UserId);
                /*
                .HasForeignKey<ChannelMessage>(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ChannelMessages_MessageId_fkey");
            */
            });
/*
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(u => u.)
                /*
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(25);

                entity.HasOne(d => d.UserNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Users_UserId_fkey");
                
            });
*/
        }
    }
}
