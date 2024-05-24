using GesEdu.ServiceLayer.Services;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IRepositories;
using GesEdu.Shared.Interfaces.ISevices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.ServiceLayer.Extensions
{
    public static class ServiceLayerDependencyInjectionExtension
    {
        public static IServiceCollection AddServiceLayerDependencies(this IServiceCollection services)
        {
            services.AddScoped<ILoginServices, LoginServices>();
            services.AddScoped<IHomepageServices, HomepageServices>();
            services.AddScoped<IUserServices, UserServices>();

            return services;
        }
    }
}
