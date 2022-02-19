namespace OwnedEntityWithUniqueIndex;

public class Child
{
    public Guid Id { get; set; }

    public Guid ParentId { get; set; }
    public string? Description { get; set; }
    public Parent Parent { get; set; } = null!;
    public Owned Owned { get; set; } = new();
}
