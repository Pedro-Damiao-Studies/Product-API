using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog;
using Microsoft.Extensions.Logging;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IoC
{
    /// <summary>
    /// Extension to add all the dependencies of the Infrastructure project
    /// </summary>
    public static class InfrastructureIoC
    {
        /// <summary>
        /// Add all the repositories to IoC
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }

        /// <summary>
        /// Add the Serilog logging to IoC
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSerilogLogging(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            services.AddSingleton(Log.Logger);

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });

            return services;
        }

        /// <summary>
        /// Add the database context to IoC
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (IsEnvironment("Testing"))
                {
                    options.UseSqlite(configuration.GetConnectionString("Testing"));
                    return;
                }

                options.UseNpgsql(configuration.GetConnectionString("Default"));
            });

            return services;
        }

        private static bool IsEnvironment(string environment)
        {
            return string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), environment, StringComparison.OrdinalIgnoreCase);
        }
    }
}
