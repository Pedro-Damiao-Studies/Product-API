using Domain.Models;
using FluentResults;
using Infrastructure.Dtos;
using Service.Dtos;

namespace Service.Services.Interfaces
{
    /// <summary>
    /// Product Service
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="productToCreate"></param>
        /// <returns></returns>
        public Task<Result<Product>> Create(CreateProductDto productToCreate);
        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productToUpdate"></param>
        /// <returns></returns>
        public Task<Result<Product>> Update(int id, UpdateProductDto productToUpdate);
        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Result> Delete (int id);
        /// <summary>
        /// Get a product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Result<Product>> GetById(int id);
        /// <summary>
        /// Find all products with filters
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public Task<Result<List<Product>>> FindAll(FindProductsDto options);
    }
}
