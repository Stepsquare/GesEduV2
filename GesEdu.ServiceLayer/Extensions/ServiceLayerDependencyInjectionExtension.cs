using GesEdu.ServiceLayer.Services;
using GesEdu.Shared.Interfaces.ISevices;
using Microsoft.Extensions.DependencyInjection;

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
