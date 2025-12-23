using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Ecommerce.Core.RepositoryContracts;
using Ecommerce.Infrastructure.DbContext;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;



namespace Ecommerce.Infrastructure
{   
    /// <summary>
    ///  Extension method for adding infra services to DI container 
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services)
        {

            services.AddTransient<IUsersRepository, UserRepository>();
            services.AddTransient<DapperDbContext>();
            return services;

        }
    }
}