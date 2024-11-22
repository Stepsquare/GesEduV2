namespace GesEdu.Helpers.Breadcrumbs
{
    public static class BreadcrumbDependencyInjectionExtension
    {
        public static IServiceCollection AddBreadcrumbService(this IServiceCollection services)
        {
            services.AddSingleton<BreadcrumbService>();

            return services;
        }
    }
}
