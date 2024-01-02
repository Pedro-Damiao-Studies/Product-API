using Domain.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Product Repository
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Create a product on database
        /// </summary>
        /// <param name="productToCreate"></param>
        /// <returns></returns>
        public Task<Product> Create(Product productToCreate);
        /// <summary>
        /// Update a product on database
        /// </summary>
        /// <param name="productToUpdate"></param>
        /// <returns></returns>
        public Task<Product> Update(Product productToUpdate);
        /// <summary>
        /// Delete a product on database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task Delete(int id);
        /// <summary>
        /// Get a product by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Product?> GetById(int id);
        /// <summary>
        /// Find all products from database with filters
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public Task<List<Product>> FindAll(FindProductsDto options);
    }
}
