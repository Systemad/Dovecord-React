using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
// TODO: Populate with user info 

public class User
{
    /*
    public User()
    {
        Users = new Collection<User>();
    }
    */
    //[Key]
    public Guid Id { get; set; }
    public string Username { get; set; }
    //public virtual ICollection<User> Users { get; set; }
    public bool Online { get; set; }
    
    public Guid MessageId { get; set; }
    public Message Message { get; set; }
    public ICollection<Message> Messages { get; set; }
    //public virtual ICollection<Message> SentMessages { get; set; }
    //public bool Admin { get; set; }
    //public string AvatarUrl { get; set; }
        
    //TODO: Add color?, and imageURL (string)
    //public Color Color { get; set; } Add other info
}