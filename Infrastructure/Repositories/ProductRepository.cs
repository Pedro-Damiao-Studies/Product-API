using Domain.Models;
using Infrastructure.Database.Context;
using Infrastructure.Dtos;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Infrastructure.Repositories
{
    /// <inheritdoc/>
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext DbContext { get; set; }

        /// <summary>
        /// Product Repository constructor
        /// </summary>
        /// <param name="context"></param>
        public ProductRepository(ApplicationDbContext context)
        {
            DbContext = context;
        }

        /// <inheritdoc/>
        public async Task<Product> Create(Product productToCreate)
        {
            await DbContext.Products.AddAsync(productToCreate);
            await DbContext.SaveChangesAsync();

            return productToCreate;
        }

        /// <inheritdoc/>
        public async Task Delete(int id)
        {
            var productToDelete = await DbContext.Products.FindAsync(id);

            if (productToDelete == null) return;

            DbContext.Products.Remove(productToDelete);
            await DbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Product>> FindAll(FindProductsDto options)
        {
            IQueryable<Product> query = DbContext.Products;

            if (!string.IsNullOrEmpty(options.NameToFind))
            {
                query = query.Where(p => EF.Functions.ILike(p.Name, $"%{options.NameToFind}%"));
            }

            if (!string.IsNullOrEmpty(options.PropertyToOrderBy))
            {
                query = query.OrderBy(options.PropertyToOrderBy);
            }

            var skip = (options.Pagination.PageNumber - 1) * options.Pagination.PageSize;
            query = query.Skip(skip).Take(options.Pagination.PageSize);

            return await query.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Product?> GetById(int id)
        {
            return await DbContext.Products.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<Product> Update(Product productToUpdate)
        {
            DbContext.Products.Update(productToUpdate);
            await DbContext.SaveChangesAsync();

            return productToUpdate;
        }
    }
}
