using Domain.Entities;

namespace Application.Interfaces;

public interface IChatService
{
    Task<List<Channel>> GetMessagesByChannelIdAsync(Guid id);
    Task<bool> DeleteMessageByIdAsync(Guid id);
    Task<bool> SaveMessageToChannelAsync(ChannelMessage id);
    Task<ChannelMessage> GetMessageByIdAsync(Guid id);
    Task<bool> UpdateMessageAsync(ChannelMessage message);
    Task<bool> UserOwnsMessageAsync(Guid postId, string userId);
}