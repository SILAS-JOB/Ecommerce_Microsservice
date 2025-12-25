
using BusinessLogicLayer.DTO;
using FluentValidation;

namespace BusinessLogicLayer.Validators ;
public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidator()
    {
        RuleFor(temp => temp.ProductID).NotEmpty().WithMessage("Product Id can't be empty");
        RuleFor(temp => temp.ProductName).NotEmpty().WithMessage("Product Name cant be blank");
        RuleFor(temp => temp.Category).IsInEnum().WithMessage("Product category must be one of the following");
        RuleFor(temp => temp.UnitPrice).InclusiveBetween(0, double.MaxValue).WithMessage($"Unit price must be between 0 and {double.MaxValue}");
        RuleFor(temp => temp.QuantityInStock).InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity in stock must be between 0 and {double.MaxValue}");
        
    }
}