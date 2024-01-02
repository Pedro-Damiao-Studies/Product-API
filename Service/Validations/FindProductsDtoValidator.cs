using Domain.Models;
using FluentValidation;
using Infrastructure.Dtos;

namespace Service.Services.Validations
{
    /// <summary>
    /// Find Products Dto Validation with FluentValidation
    /// </summary>
    public class FindProductsDtoValidator : AbstractValidator<FindProductsDto>
    {
        private readonly IEnumerable<string> _productProperties = typeof(Product).GetProperties().Select(property => property.Name);

        /// <summary>
        /// Execute Validations on FindProductsDto
        /// </summary>
        public FindProductsDtoValidator()
        {
            RuleFor(product => product.NameToFind)
                .MaximumLength(100).WithMessage($"{nameof(FindProductsDto.NameToFind)} must be less than or equal to 100 characters");

            RuleFor(product => product.PropertyToOrderBy)
                .Must((propertyToOrderBy) => propertyToOrderBy == null || IsPropertyFromProduct(propertyToOrderBy))
                .WithMessage($"{nameof(FindProductsDto.PropertyToOrderBy)} should be one of those options: {string.Join(',', _productProperties)}");

            RuleFor(product => product.Pagination)
                .NotNull().WithMessage($"{nameof(FindProductsDto.Pagination)} is required");

            RuleFor(product => product.Pagination.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage($"{nameof(FindProductsDto.Pagination.PageNumber)} must be greater than or equal to 1");

            RuleFor(product => product.Pagination.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage($"{nameof(FindProductsDto.Pagination.PageSize)} must be greater than or equal to 1")
                .LessThanOrEqualTo(200).WithMessage($"{nameof(FindProductsDto.Pagination.PageSize)} must be less than or equal to 200");
        }

        private bool IsPropertyFromProduct(string? propertyName)
        {
            return _productProperties.Contains(propertyName);
        }   
    }
}
