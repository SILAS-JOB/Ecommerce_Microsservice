using DataAcessLayer.Context;
using DataAcessLayer.Repositories;
using DataAcessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAcessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAcessLayer(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!);
        });

        services.AddScoped<IProductsRepository, ProductsRepository>();
        return services;
    }
}