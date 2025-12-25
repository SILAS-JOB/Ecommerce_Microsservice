using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.ServiceContracts;
using DataAcessLayer.RepositoryContracts;
using FluentValidationResult = FluentValidation.Results.ValidationResult;
using FluentValidation;
using DataAcessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if(productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(productAddRequest)); 
        }

        FluentValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);

        if(!validationResult.IsValid)
        {
            string errors = string.Join(", ",validationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(errors);
        }

        Product productInput = _mapper.Map<Product>(productAddRequest);
        Product? addedProduct = await _productsRepository.AddProduct(productInput);
    
        if(addedProduct == null)
        {
            return null;
        }

        ProductResponse addedProductResponse = _mapper.Map<ProductResponse>(addedProduct);

        return addedProductResponse;
    
    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        Product existingProduct = await _productsRepository.GetProductByCondition(temp => temp.ProductID == productID);
        if(existingProduct == null)
        {
            return false;
        }

        bool isDeleted = await _productsRepository.DeleteProduct(productID);
        return isDeleted;
    
    }   

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        Product? product = await _productsRepository.GetProductByCondition(conditionExpression);
        if(product == null)
        {
            return null;
        }

        ProductResponse productResponse = _mapper.Map<ProductResponse>(product);

        return productResponse;
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
        IEnumerable<Product?> products = await _productsRepository.GetProducts();

        IEnumerable<ProductResponse> productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products);

        return productResponses.ToList();
        
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        IEnumerable<Product?> products = await _productsRepository.GetProductsByCondition(conditionExpression);

        IEnumerable<ProductResponse> productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products);

        return productResponses.ToList();
        
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        Product? existingProduct = await _productsRepository.GetProductByCondition(temp => temp.ProductID == productUpdateRequest.ProductID);
    
        if(existingProduct == null)
        {
            throw new ArgumentException("Invalid Product ID");
        }

        FluentValidationResult validationResult = await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);


        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ",validationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(errors);
        }

        Product product = _mapper.Map<Product>(productUpdateRequest);   
    
        Product? productAfterUpdate = await _productsRepository.UpdateProduct(product);
    
        ProductResponse? productAfterUpdateResponse = _mapper.Map<ProductResponse>(productAfterUpdate);
        return productAfterUpdateResponse; 
    
    }
}