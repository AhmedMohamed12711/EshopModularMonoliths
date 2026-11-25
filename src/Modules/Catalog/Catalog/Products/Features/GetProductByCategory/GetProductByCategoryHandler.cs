namespace Catalog.Products.Features.GetProductByCategory;


public record GetProductByCategoryQuery(string Category):IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<ProductDto> Products);
internal class GetProductByCategoryHandler(CatalogdbContext dbContext) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .Where(p => p.Category.Contains(query.Category))
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDto = product.Adapt<List<ProductDto>>();

        return new GetProductByCategoryResult(productDto);
            
    }
}
