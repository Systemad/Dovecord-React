namespace WebUI.Dtos.User;

public class UserCreationTest
{
    public Guid Id { get; set; } = new Guid();
    public string? Name { get; set; }
    public bool? IsOnline { get; set; }
}