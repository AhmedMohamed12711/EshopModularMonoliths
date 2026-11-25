using Shared.Data.Seed;

namespace Catalog.Data.Seed;

public  class CatalogDataSeeding(CatalogdbContext dbcontext) : IDataSeeder
{
    public  async Task SeedAllAsync()
    {
       if(!await dbcontext.Products.AnyAsync())
        {
            await dbcontext.Products.AddRangeAsync(IntialData.Products);
            await dbcontext.SaveChangesAsync();
        }
    }
}
