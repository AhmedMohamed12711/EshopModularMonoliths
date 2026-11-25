
using Catalog.Products.Features.UpdateProduct;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.Products.Features.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);
internal class DeleteProductHandler(CatalogdbContext dbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.
           FindAsync([command.ProductId], cancellationToken: cancellationToken);
        if (product == null)
        {
            throw new ArgumentException($"Product not found :{command.ProductId}");
        }


        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
