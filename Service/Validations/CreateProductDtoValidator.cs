using FluentValidation;
using Service.Dtos;

namespace Service.Services.Validations
{
    /// <summary>
    /// Create Product Dto Validation with FluentValidation
    /// </summary>
    public class CreateProductDtoValidator: AbstractValidator<CreateProductDto>
    {
        /// <summary>
        /// Execute Validations on CreateProductDto
        /// </summary>
        public CreateProductDtoValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage($"{nameof(CreateProductDto.Name)} is required")
                .MaximumLength(100).WithMessage($"{nameof(CreateProductDto.Name)} must be less than or equal to 100 characters");

            RuleFor(product => product.Description)
                .NotEmpty().WithMessage($"{nameof(CreateProductDto.Description)} is required")
                .MaximumLength(500).WithMessage($"{nameof(CreateProductDto.Description)} must be less than or equal to 500 characters");

            RuleFor(product => product.AvailableQuantity)
                .NotEmpty().WithMessage($"{nameof(CreateProductDto.AvailableQuantity)} is required")
                .GreaterThanOrEqualTo(0).WithMessage($"{nameof(CreateProductDto.AvailableQuantity)} must be greater than or equal to 0");

            RuleFor(product => product.Price)
                .NotEmpty().WithMessage($"{nameof(CreateProductDto.Price)} is required")
                .GreaterThanOrEqualTo(0).WithMessage($"{nameof(CreateProductDto.Price)} must be greater than or equal to 0");
        }   
    }
}
