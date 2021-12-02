using Domain.Channels;
using Domain.Entities;
using Domain.Messages;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IDoveDbContext
{
    DbSet<Channel> TextChannels { get; }
    DbSet<ChannelMessage> ChannelMessages { get; }
    DbSet<User> Users { get; }
}