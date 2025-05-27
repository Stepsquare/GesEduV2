using GesEdu.Shared.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace GesEdu.Helpers.ExportPdf.Models
{
    public class PdfExportModel<T> where T : class
    {
        public string CodServico { get; set; }
        public string NomeServico { get; set; }
        public required T Data { get; set; }
        public string BaseUrl { get; set; }

        [SetsRequiredMembers]
        public PdfExportModel(T data, HttpContext httpContext)
        {
            Data = data;
            BaseUrl = GetBaseUrl(httpContext);
            CodServico = httpContext.User.GetCodigoServico();
            NomeServico = httpContext.User.GetNomeServico();
        }

        public Dictionary<string, object?> GetAsDictionary()
        {
            return new Dictionary<string, object?>
            {
                { "Model", this }
            };
        }

        private string GetBaseUrl(HttpContext httpContext)
        {
            var request = (httpContext?.Request) ?? throw new ArgumentNullException();

            var host = request.Host.ToUriComponent();

            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }
    }
}
