namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
public class CreateProductHandler(CatalogdbContext dbcontext)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreatNewProduct(command.Product);
        dbcontext.Products.Add(product);
        await dbcontext.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);

    }

    private Product CreatNewProduct(ProductDto productDto)
    {
        var product = Product.Create(
             Guid.NewGuid(),
             productDto.Name,
             productDto.Category,
             productDto.Description,
             productDto.ImageFile,
             productDto.Price
             );
        return product;



    }
}
