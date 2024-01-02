using Domain.Models;
using FluentResults;
using FluentValidation;
using Infrastructure.Dtos;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Service.Dtos;
using Service.Extensions;
using Service.Services.Interfaces;

namespace Service.Services
{
    /// <summary>
    /// Product Service
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="createProductDtoValidator"></param>
    /// <param name="updateProductDtoValidator"></param>
    /// <param name="findProductsDtoValidator"></param>
    /// <param name="productRepository"></param>
    public class ProductService(
            ILogger<ProductService> logger,
            IValidator<CreateProductDto> createProductDtoValidator,
            IValidator<UpdateProductDto> updateProductDtoValidator,
            IValidator<FindProductsDto> findProductsDtoValidator,
            IProductRepository productRepository
            ) : IProductService
    {
        private readonly ILogger<ProductService> _logger = logger;
        private readonly IValidator<CreateProductDto> _createProductDtoValidator = createProductDtoValidator;
        private readonly IValidator<UpdateProductDto> _updateProductDtoValidator = updateProductDtoValidator;
        private readonly IValidator<FindProductsDto> _findProductsDtoValidator = findProductsDtoValidator;
        private readonly IProductRepository _productRepository = productRepository;

        /// <inheritdoc/>
        public async Task<Result<Product>> Create(CreateProductDto productToCreate)
        {
            try
            {
                var validationResult = _createProductDtoValidator.Validate(productToCreate);
                if (!validationResult.IsValid)
                    return validationResult.GetFluentErrors();

                var newProduct = new Product(productToCreate.Name, productToCreate.Description, productToCreate.AvailableQuantity, productToCreate.Price);

                var createdProduct = await _productRepository.Create(newProduct);
                return Result.Ok(createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return Result.Fail(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<Result<Product>> Update(int id, UpdateProductDto productToUpdate)
        {
            try
            {
                var validationResult = _updateProductDtoValidator.Validate(productToUpdate);
                if (!validationResult.IsValid)
                    return validationResult.GetFluentErrors();

                var product = await _productRepository.GetById(id);

                if (product == null)
                    return Result.Fail("Product not found");

                product.Id = product.Id;
                product.Name = productToUpdate.Name ?? product.Name;
                product.Description = productToUpdate.Description ?? product.Description;
                product.AvailableQuantity = productToUpdate.AvailableQuantity ?? product.AvailableQuantity;
                product.Price = productToUpdate.Price ?? product.Price;

                var updatedProduct =  await _productRepository.Update(product);
                return Result.Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product | Id: {productId}", id);
                return Result.Fail(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<Result> Delete(int id)
        {
            try
            {
                if (await _productRepository.GetById(id) == null)
                    return Result.Fail("Product not found");

                await _productRepository.Delete(id);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product | Id: {productId}", id);
                return Result.Fail(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<Result<Product>> GetById(int id)
        {
            try
            {
                var product = await _productRepository.GetById(id);

                if (product == null)
                    return Result.Fail("Product not found");

                return Result.Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product | Id: {productId}", id);
                return Result.Fail(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<Result<List<Product>>> FindAll(FindProductsDto options)
        {
            try
            {
                var validationResult = _findProductsDtoValidator.Validate(options);
                if (!validationResult.IsValid)
                    return validationResult.GetFluentErrors();

                options.PropertyToOrderBy ??= nameof(Product.Id);

                var products = await _productRepository.FindAll(options);
                return Result.Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products | Options: {@findAllOptions}", options);
                return Result.Fail(ex.Message);
            }
        }
    }
}
