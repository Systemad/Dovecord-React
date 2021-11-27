using Domain.Entities;

namespace Application.Interfaces;

public interface IChatService
{
    Task<List<Message>> GetMessagesByChannelIdAsync(Guid id);
    Task<bool> DeleteMessageByIdAsync(Guid id);
    Task<bool> SaveMessageToChannelAsync(Message id);
    Task<Message> GetMessageByIdAsync(Guid id);
    Task<bool> UpdateMessageAsync(Message message);
    Task<bool> UserOwnsMessageAsync(Guid postId, string userId);
}