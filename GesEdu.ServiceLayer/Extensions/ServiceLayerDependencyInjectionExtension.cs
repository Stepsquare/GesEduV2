using GesEdu.ServiceLayer.Services;
using GesEdu.ServiceLayer.Services.SIME;
using GesEdu.Shared.Interfaces.IServices;
using GesEdu.Shared.Interfaces.IServices.SIME;
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

            #region SIME

            services.AddScoped<IApreciacaoManuaisServices, ApreciacaoManuaisServices>();

            #endregion
            
            return services;
        }
    }
}
