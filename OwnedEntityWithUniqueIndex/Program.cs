using (var context = new OwnedEntityContext())
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    var parent = new Parent { Id = Guid.NewGuid() };
    context.Add(parent);

    var child = new Child
    {
        Id = Guid.NewGuid(),
        Parent = parent,
        Owned = new() { Value = 3 }
    };
    context.Add(child);

    context.Entry(child).Reference(e => e.Owned).TargetEntry!.Property<Guid>(nameof(Child.ParentId))
    .CurrentValue = child.ParentId;

    context.SaveChanges();
}
