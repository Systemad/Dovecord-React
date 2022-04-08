namespace Dovecord.Domain.Users.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public bool? IsOnline { get; set; }
    public bool Bot { get; set; }
    public bool? System { get; set; }
    public bool? AccentColor { get; set; }
}