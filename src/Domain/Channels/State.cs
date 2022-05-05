using Domain.Messages;

namespace Domain.Channels;

public partial class ChannelState
{
    public Guid Id { get; set; }
    public bool Created { get; set; }
    public string Name { get; set; }
    public List<ChannelMessage> Messages { get; set; }
    public Guid? ServerId { get; set; }

    public ChannelState()
    {
        Name = string.Empty;
        Messages = new List<ChannelMessage>();
    }

    public ChannelState(Guid id, bool created, string name, List<ChannelMessage> messages, Guid serverId)
    {
        Id = id;
        Created = created;
        Name = name;
        Messages = messages;
        ServerId = serverId;
    }

    public void Apply(ChannelCreatedEvent evt)
    {
        Id = evt.Channel.Id;
        Name = evt.Channel.Name;
        ServerId = evt.Channel.ServerId;
        Created = true;
    }

    public void Apply(MessageAddedEvent evt) => Messages.Add(evt.Message);
}