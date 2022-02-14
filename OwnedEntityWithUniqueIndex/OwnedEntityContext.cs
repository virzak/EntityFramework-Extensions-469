namespace OwnedEntityWithUniqueIndex;

public class OwnedEntityContext : DbContext
{
    public DbSet<Parent> Parents { get; set; } = default!;
    public DbSet<Child> Children { get; set; } = default!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OwnedEntityWithUniqueIndex;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Child>().Property(c => c.ParentId)
         .HasColumnName(nameof(Child.ParentId))
         .IsRequired();

        modelBuilder.Entity<Child>().OwnsOne(c => c.Owned, o =>
        {
            o.Property<Guid>(nameof(Child.ParentId))
             .HasColumnName(nameof(Child.ParentId))
             .IsRequired();

            o.HasIndex(nameof(Child.ParentId), nameof(Owned.Value))
            .IsUnique();
        });
    }
}
