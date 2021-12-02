using System.ComponentModel.DataAnnotations;

namespace Domain.Messages;

public abstract class BaseMessageEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public bool IsEdit { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}