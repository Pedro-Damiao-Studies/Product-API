using Domain.Models;
using Infrastructure.Database.Context.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Context
{
    /// <summary>
    /// DbContext of application
    /// </summary>
    /// <param name="options"></param>
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        /// <summary>  Products table </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>  Configure the model behavior on creating </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductMap());
        }
    }
}
