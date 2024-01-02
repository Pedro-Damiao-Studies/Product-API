using FluentValidation;
using Service.Dtos;

namespace Service.Services.Validations
{
    /// <summary>
    /// Update Product Dto Validation with FluentValidation
    /// </summary>
    public class UpdateProductDtoValidator: AbstractValidator<UpdateProductDto>
    {
        /// <summary>
        /// Execute validations on UpdateProductDto
        /// </summary>
        public UpdateProductDtoValidator()
        {
            RuleFor(product => product.Name)
                .MaximumLength(100).WithMessage($"{nameof(UpdateProductDto.Name)} must be less than or equal to 100 characters");

            RuleFor(product => product.Description)
                .MaximumLength(500).WithMessage($"{nameof(UpdateProductDto.Description)} must be less than or equal to 500 characters");

            RuleFor(product => product.AvailableQuantity)
                .GreaterThanOrEqualTo(0).WithMessage($"{nameof(UpdateProductDto.AvailableQuantity)} must be greater than or equal to 0");

            RuleFor(product => product.Price)
                .GreaterThanOrEqualTo(0).WithMessage($"{nameof(UpdateProductDto.Price)} must be greater than or equal to 0");
        }   
    }
}
