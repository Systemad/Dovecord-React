using System.ComponentModel.DataAnnotations;

namespace WebUI.Domain.Messages;

public abstract class BaseMessageEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedOn { get; set; }
    public Guid? CreatedBy { get; set; }
    public bool IsEdit { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}