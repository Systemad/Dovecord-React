// <auto-generated />
using System;
using Application.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Application.Migrations
{
    [DbContext(typeof(DoveDbContext))]
    partial class DoveDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Channels.Channel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ServerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Topic")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("Domain.Messages.ChannelMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChannelId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsEdit")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ServerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ChannelId");

                    b.HasIndex("ServerId");

                    b.ToTable("ChannelMessages");
                });

            modelBuilder.Entity("Domain.Servers.Server", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("IconUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool?>("AccentColor")
                        .HasColumnType("boolean");

                    b.Property<bool>("Bot")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ChannelId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastOnline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("System")
                        .HasColumnType("boolean");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Users.UserSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("ServerUser", b =>
                {
                    b.Property<Guid>("MembersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServersId")
                        .HasColumnType("uuid");

                    b.HasKey("MembersId", "ServersId");

                    b.HasIndex("ServersId");

                    b.ToTable("ServerUser");
                });

            modelBuilder.Entity("Domain.Channels.Channel", b =>
                {
                    b.HasOne("Domain.Servers.Server", "Server")
                        .WithMany("Channels")
                        .HasForeignKey("ServerId");

                    b.Navigation("Server");
                });

            modelBuilder.Entity("Domain.Messages.ChannelMessage", b =>
                {
                    b.HasOne("Domain.Users.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Channels.Channel", "Channel")
                        .WithMany("Messages")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Servers.Server", "Server")
                        .WithMany()
                        .HasForeignKey("ServerId");

                    b.Navigation("Author");

                    b.Navigation("Channel");

                    b.Navigation("Server");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.HasOne("Domain.Channels.Channel", null)
                        .WithMany("Recipients")
                        .HasForeignKey("ChannelId");
                });

            modelBuilder.Entity("Domain.Users.UserSettings", b =>
                {
                    b.HasOne("Domain.Users.User", "User")
                        .WithOne("UserSettings")
                        .HasForeignKey("Domain.Users.UserSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ServerUser", b =>
                {
                    b.HasOne("Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Servers.Server", null)
                        .WithMany()
                        .HasForeignKey("ServersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Channels.Channel", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Recipients");
                });

            modelBuilder.Entity("Domain.Servers.Server", b =>
                {
                    b.Navigation("Channels");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Navigation("UserSettings")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
