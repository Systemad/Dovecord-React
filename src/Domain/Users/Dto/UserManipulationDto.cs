namespace Domain.Users.Dto;

public class UserManipulationDto
{
    public string? Username { get; set; }
    public bool Bot { get; set; }
    public bool? System { get; set; }
    public bool? AccentColor { get; set; }
}

public class UserSettingsManipulationDto
{
    public string? Bio { get; set; }
    public PresenceStatus? PresenceStatus { get; set; }
    public CustomStatus? CustomStatus { get; set; }
}