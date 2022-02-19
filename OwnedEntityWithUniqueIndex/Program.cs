var parent = new Parent { Id = Guid.NewGuid() };

using (var context = new OwnedEntityContext())
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    context.Add(parent);

    context.SaveChanges();
}

using (var context = new OwnedEntityContext())
{
    var child = new Child
    {
        Id = Guid.NewGuid(),
        ParentId = parent.Id,
        Owned = new() { Value = 3 }
    };
    context.Add(child);

    // Only need this when using SaveChanges()
    //context.Entry(child).Reference(e => e.Owned).TargetEntry!.Property<Guid>(nameof(Child.ParentId))
    //.CurrentValue = child.ParentId;

    context.BulkInsert(new[] { child });
}
