using Domain.Channels;
using Domain.Entities;

namespace Application.Interfaces;

public interface IChannelService
{
    Task<List<Channel>> GetChannels();
    Task<bool> DeleteChannelAsync(Guid id);
    Task<bool> CreateChannelAsync(Channel id);
    Task<Channel> GetChannelByIdAsync(Guid id);
    Task<bool> UpdateChannelAsync(Channel message);
}