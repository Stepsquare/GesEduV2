using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IServices.SIME;
using GesEdu.Shared.WebserviceModels.SIME;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace GesEdu.ServiceLayer.Services.SIME
{
    public class NovasAdocoesServices(
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory,
        IUnitOfWork unitOfWork,
        IHostEnvironment environment) : SIMEBaseServices(httpContextAccessor, httpClientFactory, unitOfWork, environment), INovasAdocoesServices
    {
        public async Task<GetManuaisAdotadosListagemResponse?> GetManuaisAdotadosPdfExport(string cod_escola_me, string tipologia)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getManuaisAdotadosListagem");


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
            request.Headers.Add("cod_escola_filtro", cod_escola_me);
            request.Headers.Add("tipologia", tipologia);

            var response = await SendAsync<GetManuaisAdotadosListagemResponse>(request) ?? throw new Exception("Erro ao descarregar o ficheiro.");

            if (response.MSG_BUTTON == "N")
                throw new Exception("Atualize as estimativas de alunos na página de adoções de anos anteriores.");

            return response;
        }
    }
}
