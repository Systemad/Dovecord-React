using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ChatService : IChatService
{
    private DoveDbContext _context;
        
    public ChatService(DoveDbContext context)
    {
        _context = context;
    }

    // TODO: Check
    public async Task<List<Channel>> GetMessagesByChannelIdAsync(Guid id)
    {
        return await _context.Channels
            .Where(a => a.ChannelId == id)
            .Include(m => m.ChannelMessages)
            .ToListAsync();
    }

    public async Task<bool> DeleteMessageByIdAsync(Guid id)
    {
        var message = await _context.ChannelMessages.Where(x => x.ChannelMessageId == id)
            .AsTracking().SingleOrDefaultAsync();
        _context.ChannelMessages.Remove(message);
        var deleted = await _context.SaveChangesAsync();
        return deleted > 0;
    }

    public async Task<bool> SaveMessageToChannelAsync(ChannelMessage message)
    {
        await _context.ChannelMessages.AddAsync(message);
        var created = await _context.SaveChangesAsync();
        return created > 0;
    }

    public async Task<ChannelMessage> GetMessageByIdAsync(Guid id)
    {
        return await _context.ChannelMessages.SingleOrDefaultAsync(x => x.ChannelMessageId == id);
    }

    public async Task<bool> UpdateMessageAsync(ChannelMessage message)
    {
        var messageToUpdate = await _context.ChannelMessages.Where(x => x.ChannelMessageId == message.ChannelMessageId)
            .AsTracking().SingleOrDefaultAsync();
 
        messageToUpdate.Content = message.Content;
        messageToUpdate.IsEdit = true;
        
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }

    public async Task<bool> UserOwnsMessageAsync(Guid messageId, string userId)
    {
        var post = await GetMessageByIdAsync(messageId);
        if (post is null)
            return false;
        return post.User.UserId == Guid.Parse(userId);
    }
}