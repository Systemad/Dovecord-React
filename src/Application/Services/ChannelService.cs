using System.Threading.Channels;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Channel = Domain.Channels.Channel;

namespace Application.Services;

public class ChannelService : IChannelService
{
    private DoveDbContext _context;
        
    public ChannelService(DoveDbContext context)
    {
        _context = context;
    }

    // TODO: Check
    public async Task<List<Channel>> GetMessagesByChannelIdAsync(Guid id)
    {
        return await _context.Channels
            .Where(a => a.Id == id)
            .Include(m => m.ChannelMessages)
            .ToListAsync();
    }

    public async Task<List<Channel>> GetChannels()
    {
        return await _context.Channels.ToListAsync();
    }

    public async Task<bool> DeleteChannelAsync(Guid id)
    {
        var channel = await GetChannelByIdAsync(id);
        _context.Channels.Remove(channel);
        var deleted = await _context.SaveChangesAsync();
        return deleted > 0;
    }

    public async Task<bool> CreateChannelAsync(Channel channel)
    {
        var count = await _context.Channels.CountAsync();
        if (count > 10)
            return false;
        
        // Add error code
        await _context.Channels.AddAsync(channel);
        var created = await _context.SaveChangesAsync();
        return created > 0;
    }

    public async Task<Channel> GetChannelByIdAsync(Guid id)
    {
        return await _context.Channels.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateChannelAsync(Channel channel)
    {
        var channelToUpdate = await _context.Channels.Where(x => x.Id == channel.Id)
            .AsTracking().SingleOrDefaultAsync();
        channelToUpdate.Name = channel.Name;
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }
}