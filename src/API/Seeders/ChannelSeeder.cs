using AutoBogus;
using Infrastructure.Database;
using Domain.Channels;

namespace Dovecord.Seeders;

public static class ChannelSeeder
{
    public static void SeedSampleChannels(DoveDbContext context)
    {
        if (!context.Channels.Any())
        {
            context.Channels.Add(new AutoFaker<Channel>().Ignore(x => x.Recipients));
            context.Channels.Add(new AutoFaker<Channel>().Ignore(x => x.Recipients));
            context.Channels.Add(new AutoFaker<Channel>().Ignore(x => x.Recipients));
            context.SaveChanges();
        }
    }
}