using System;
using System.Collections.Generic;

/*
namespace Dovecord.Shared
{
    public sealed class PrivateUser
    {
        public ICollection<PrivateMessage> ChatMessagesFromUsers { get; set; }
        public ICollection<PrivateMessage> ChatMessagesToUsers { get; set; }
        
        public PrivateUser()
        {
            ChatMessagesFromUsers = new HashSet<PrivateMessage>();
            ChatMessagesToUsers = new HashSet<PrivateMessage>();
        }
    }
    
    public class PrivateMessage
    {
        public long ChatId { get; set; }
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set; }
    }
}

*/