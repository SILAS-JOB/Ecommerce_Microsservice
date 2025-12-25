using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using MySqlX.XDevAPI.Common;
using FluentValidationResult = FluentValidation.Results.ValidationResult;
namespace Products.Api.ApiEndopoints;

public static class ProductApiEndpoints
{
    public static IEndpointRouteBuilder MapProductApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async (IProductsService productsService) =>
        {
            List<ProductResponse?> products = await productsService.GetProducts();
            return Results.Ok(products);
        });

        app.MapGet("/api/products/search/product-id/{ProductID:guid}", async (IProductsService productsService, Guid ProductID) =>
        {
            ProductResponse? products = await productsService.GetProductByCondition(temp => temp.ProductID == ProductID);
            return Results.Ok(products);
        });

        app.MapGet("/api/products/search/product-id/{SearchString}", async (IProductsService productsService, string SearchString) =>
        {
            List<ProductResponse?> productsByProductName = await productsService.GetProductsByCondition(temp =>
            temp.ProductName != null && temp.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            List<ProductResponse?> productsByCategory = await productsService.GetProductsByCondition(temp =>
            temp.Category != null && temp.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            var products = productsByProductName.Union(productsByCategory);
        });

        app.MapPost("/api/products", async (IProductsService productsService, IValidator<ProductAddRequest> productAddRequestValidator, ProductAddRequest productAddRequest ) =>
        {
            FluentValidationResult validationResult = await productAddRequestValidator.ValidateAsync(productAddRequest);

            if(!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors
                    .GroupBy(temp => temp.PropertyName)
                    .ToDictionary(grp => grp.Key,
                        grp => grp.Select(err => err.ErrorMessage). ToArray());
                return Results.ValidationProblem(errors);
            }
        });

        app.MapDelete("/api/products/{ProductID:guid}", async (IProductsService productsService, Guid ProductID) =>
        {
           bool isDeleted = await productsService.DeleteProduct(ProductID);
            if(isDeleted)
                return Results.Ok(true);
            else
                return Results.Problem("Error deleting product");
        });

        return app;
    }
}