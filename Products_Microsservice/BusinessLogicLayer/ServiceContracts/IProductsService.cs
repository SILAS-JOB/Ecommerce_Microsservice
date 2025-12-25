
using System.Linq.Expressions;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.ServiceContracts;
public interface IProductsService
{
    Task<List<ProductResponse?>> GetProducts();
    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>>conditionExpression);
    Task<List<ProductResponse?>> GetProductByCondition(Expression<Func<Product, bool>>conditionExpression); 
    Task<List<ProductResponse?>> AddProduct(ProductAddRequest productAddRequest);
    Task<List<ProductResponse?>> UpdateProduct(ProductUpdateRequest productUpdateRequest);
    Task<bool> DeleteProduct(Guid productID);

}