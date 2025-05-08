using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IServices.SIME;
using GesEdu.Shared.WebserviceModels.Adm;
using GesEdu.Shared.WebserviceModels.SIME;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace GesEdu.ServiceLayer.Services.SIME
{
    public abstract class SIMEBaseServices(
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory,
        IUnitOfWork unitOfWork,
        IHostEnvironment environment) : BaseServices(httpContextAccessor, httpClientFactory, unitOfWork, environment), ISIMEBaseServices
    {
        public async Task<List<GetCiclosUOResponseItem>?> GetCiclos()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getCiclosUO");

            if (_httpContext.User.IsEscolaPrivada())
            {
                request.Headers.Add("cod_uo", "0");
                request.Headers.Add("cod_escola_me", _httpContext.User.GetCodigoServico());
            }
            else
            {
                request.Headers.Add("cod_uo", _httpContext.User.GetCodigoServico());
            }

            request.Headers.Add("id_ano_letivo", _httpContext.User.GetAnoLetivoSIME());
            request.Headers.Add("utilizador", _httpContext.User.GetUsername());

            return await SendAsync<List<GetCiclosUOResponseItem>>(request);
        }

        public async Task<List<GetEscolasResponse.Escola>?> GetEscolas()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getEscolas");

            if (_httpContext.User.IsEscolaPrivada())
            {
                request.Headers.Add("cod_uo", "0");
                request.Headers.Add("cod_escola_me", _httpContext.User.GetCodigoServico());
            }
            else
            {
                request.Headers.Add("cod_uo", _httpContext.User.GetCodigoServico());
            }

            request.Headers.Add("id_ano_letivo", _httpContext.User.GetAnoLetivoSIME());
            request.Headers.Add("utilizador", _httpContext.User.GetUsername());

            var result = await SendAsync<GetEscolasResponse>(request);

            return result?.escolas;
        }
    }
}
