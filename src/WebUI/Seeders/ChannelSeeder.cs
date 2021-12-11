using AutoBogus;
using WebUI.Databases;
using WebUI.Domain.Channels;

namespace WebUI.Seeders;

public static class ChannelSeeder
{
    public static void SeedSampleChannels(DoveDbContext context)
    {
        if (!context.Channels.Any())
        {
            context.Channels.Add(new AutoFaker<Channel>());
            context.Channels.Add(new AutoFaker<Channel>());
            context.Channels.Add(new AutoFaker<Channel>());
            context.SaveChanges();
        }
    }
}