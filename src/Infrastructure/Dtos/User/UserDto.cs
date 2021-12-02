namespace Infrastructure.Dtos.User;

public class UserDto
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public bool? IsOnline { get; set; }
}