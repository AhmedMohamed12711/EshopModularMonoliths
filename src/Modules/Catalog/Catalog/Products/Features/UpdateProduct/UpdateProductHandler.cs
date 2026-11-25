namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDto Product):ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSucess);
public class UpdateProductHandler(CatalogdbContext dbContext) 
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product =await dbContext.Products.
            FindAsync([command.Product.Id], cancellationToken: cancellationToken);
        if(product == null)
        {
            throw new ArgumentException($"Product not found :{command.Product.Id}");
        }
        UpdateProductWithNewValue(product,command.Product);
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }

    private void UpdateProductWithNewValue(Product product, ProductDto productDto)
    {
        product.Update(
            productDto.Name,
            productDto.Category,
            productDto.Description,
            productDto.ImageFile,
            productDto.Price
            );
    }
}
