using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAcessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAcessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}