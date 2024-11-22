using Microsoft.AspNetCore.Components.Web;

namespace GesEdu.Helpers.ExportPdf.Extensions
{
    public static class PdfExportDependencyInjectionExtension
    {
        public static IServiceCollection AddPdfExportDependencies(this IServiceCollection services)
        {
            services.AddScoped<HtmlRenderer>();
            services.AddScoped<PdfRenderer>();

            return services;
        }
    }
}
