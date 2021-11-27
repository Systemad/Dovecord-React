using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IDoveDbContext
{
    DbSet<Channel> TextChannels { get; }
    DbSet<Message> ChannelMessages { get; }
    DbSet<User> Users { get; }
}