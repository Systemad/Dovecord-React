﻿// <auto-generated />
using System;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DoveDbContext))]
    [Migration("20211201201502_Updatemodels")]
    partial class Updatemodels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Channel", b =>
                {
                    b.Property<Guid>("ChannelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ChannelId");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("Domain.Entities.ChannelMessage", b =>
                {
                    b.Property<Guid>("ChannelMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChannelForeignKey")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<bool?>("IsEdit")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserForeignKey")
                        .HasColumnType("uuid");

                    b.HasKey("ChannelMessageId");

                    b.HasIndex("ChannelForeignKey");

                    b.HasIndex("UserForeignKey");

                    b.ToTable("ChannelMessages");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool?>("IsOnline")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.ChannelMessage", b =>
                {
                    b.HasOne("Domain.Entities.Channel", "Channel")
                        .WithMany("ChannelMessages")
                        .HasForeignKey("ChannelForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("SentMessages")
                        .HasForeignKey("UserForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Channel", b =>
                {
                    b.Navigation("ChannelMessages");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("SentMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
