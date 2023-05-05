namespace backend.Models.Common;
// <summary>
// Base record for all entities in the system, it is and ID and a CreatedAt property
// </summary>

public record EntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}