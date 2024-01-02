using FluentResults;
using FluentValidation.Results;

namespace Service.Extensions
{
    /// <summary>
    /// Fluet Validations Result Extensions
    /// </summary>
    public static class ValidationResultExtensions
    {
        /// <summary>
        /// Get Fluent Errors
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns></returns>
        public static Result GetFluentErrors(this ValidationResult validationResult)
        {
            if (validationResult.IsValid)
                return Result.Ok();

            var errorsByPropertyName = validationResult.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage).ToList()
                );

            var errors = new List<Error>();

            foreach (var propertyName in errorsByPropertyName.Keys)
            {
                var errorMessages = errorsByPropertyName[propertyName];
                var error = new Error($"Invalid field: {propertyName}").CausedBy(errorMessages);
                errors.Add(error);
            }

            return Result.Fail(errors);
        }
    }
}
