using GesEdu.DataLayer.Repositories;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GesEdu.DataLayer.Extensions
{
    public static class DataLayerDependencyInjectionExtension
    {
        public static IServiceCollection AddDataLayerDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext 
            services.AddDbContext<GesEduDbContext>(x => 
                x.UseSqlServer(configuration.GetConnectionString("SqlServerConnection") ?? throw new ArgumentNullException()));

            //Repositories
            services.AddScoped<IWebServiceErrorRepository, WebServiceErrorRepository>();

            //Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
