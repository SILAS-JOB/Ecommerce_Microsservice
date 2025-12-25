using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.ServiceContracts;
using DataAcessLayer.RepositoryContracts;
using FluentValidationResult = FluentValidation.Results.ValidationResult;
using FluentValidation;

namespace BusinessLogicLayer.Services;

public class ProductServices : IProductsService
{
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;

    public ProductServices(IValidator<ProductAddRequest> productAddRequestValidator,
        IValidator<ProductUpdateRequest> productUpdateRequestValidator,
        IMapper mapper,
        IProductsRepository productsRepository )
    {
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
        _mapper = mapper;
        _productsRepository = productsRepository;
    }

    public async Task<List<ProductResponse?>> AddProduct(ProductAddRequest productAddRequest)
    {
        if(productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(productAddRequest)); 
        }

        FluentValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);
    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        
    }

    public async Task<List<ProductResponse?>> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
    }

    public async Task<List<ProductResponse?>> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
    }
}