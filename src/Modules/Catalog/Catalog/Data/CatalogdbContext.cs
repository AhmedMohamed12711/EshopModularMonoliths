namespace Catalog.Data;

public class CatalogdbContext:DbContext
{
    public CatalogdbContext(DbContextOptions<CatalogdbContext> options) : base(options) { }
    public DbSet<Product> Products=>Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.HasDefaultSchema("catalog");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
