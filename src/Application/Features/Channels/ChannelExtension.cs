using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Channels;

public static class ChannelExtension
{
    public static IQueryable<TextChannel> GetAllData(this DbSet<TextChannel> textChannels) =>
        textChannels
            //.Include(x => x.Id)
            .Include(x => x.Name);
}