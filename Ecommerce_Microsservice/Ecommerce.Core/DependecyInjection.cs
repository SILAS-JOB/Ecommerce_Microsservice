using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Core.ServiceContracts;
using Ecommerce.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Ecommerce.Core.Validators;

namespace Ecommerce.Core
{
    /// <summary>
    ///  Extension method for adding core services to DI container 
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();


            return services;
        }
    }
}
