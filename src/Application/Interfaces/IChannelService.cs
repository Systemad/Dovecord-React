using Domain.Entities;

namespace Application.Interfaces;

public interface IChannelService
{
    Task<List<TextChannel>> GetChannels();
    Task<bool> DeleteChannelAsync(Guid id);
    Task<bool> CreateChannelAsync(TextChannel id);
    Task<TextChannel> GetChannelByIdAsync(Guid id);
    Task<bool> UpdateChannelAsync(TextChannel message);
}