using FluentValidation;
using Infrastructure.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Service.Dtos;
using Service.Services;
using Service.Services.Interfaces;
using Service.Services.Validations;

namespace Service.IoC
{
    /// <summary>
    /// Services IoC
    /// </summary>
    public static class ServicesIoC
    {
        /// <summary>
        /// Add validators to IoC
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateProductDto>, CreateProductDtoValidator>();
            services.AddScoped<IValidator<UpdateProductDto>, UpdateProductDtoValidator>();
            services.AddScoped<IValidator<FindProductsDto>, FindProductsDtoValidator>();

            return services;
        }

        /// <summary>
        /// Add services to IoC
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
