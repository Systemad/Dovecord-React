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
        
    public Guid Id { get; set; }
    public string Username { get; set; }
    //public virtual ICollection<User> Users { get; set; }
    public bool Online { get; set; }
    //public bool Admin { get; set; }
    //public string AvatarUrl { get; set; }
        
    //TODO: Add color?, and imageURL (string)
    //public Color Color { get; set; } Add other info
}