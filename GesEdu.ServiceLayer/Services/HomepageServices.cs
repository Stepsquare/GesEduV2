using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.ISevices;
using GesEdu.Shared.WebserviceModels.Noticias;
using Microsoft.AspNetCore.Http;

namespace GesEdu.ServiceLayer.Services
{
    public class HomepageServices(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IUnitOfWork unitOfWork) : BaseServices(httpContextAccessor, httpClientFactory, unitOfWork), IHomepageServices
    {
        public async Task<GetNoticiasResponse?> GetNoticias(string? idServico = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get , "noticias/getNoticias");

            request.Headers.Add("show_image", "N");
            request.Headers.Add("estado", "A");

            if (!string.IsNullOrEmpty(idServico))
                request.Headers.Add("Id_servico", idServico);

            return await SendAsync<GetNoticiasResponse>(request);
        }
    }
}
