using BusinessLogicLayer.Entities;
using DataAcessLayer.Context;
using DataAcessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly ApplicationDbContext _dbcontext;


    public ProductsRepository(ApplicationDbContext dbContext)
    {
        _dbcontext = dbContext;
    }


    public async Task<Product?> AddProduct(Product product)
    {
        _dbcontext.Products.Add(product);
        await _dbcontext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        Product? existingProduct = await _dbcontext.Products.FirstOrDefaultAsync(temp => temp.ProductID == productID);
        if(existingProduct == null)
        {
            return false;
        }
        
        _dbcontext.Products.Remove(existingProduct);
        int affectedRowCount = await _dbcontext.SaveChangesAsync();
        return affectedRowCount > 0;
    }

    public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await _dbcontext.Products.FirstOrDefaultAsync(conditionExpression);
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _dbcontext.Products.ToListAsync();
    }

    public Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await _dbContext.Products.Where(conditionExpression).ToListAsync();
    }

    public Task<Product?> UpdateProduct(Product product)
    {
        Product? existingProduct = await _dbContext.Products.FirstOrDefaultAsync(temp => temp.ProductID == product.ProductID);
        if (existingProduct == null )
        {
        return null;
        }

        existingProduct.ProductName = product.ProductName;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.QuantityInStock = product.QuantityInStock;
        existingProduct.Category = product.Category;

        await _dbContext.SaveChangesAsync();

        return existingProduct;
    }
}
