using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IServices.SIME;
using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams.SIME;
using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.SIME;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;

namespace GesEdu.ServiceLayer.Services.SIME
{
    public class AdocoesAnosAnterioresServices(
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory,
        IUnitOfWork unitOfWork,
        IHostEnvironment environment) : SIMEBaseServices(httpContextAccessor, httpClientFactory, unitOfWork, environment), IAdocoesAnosAnterioresServices
    {
        public async Task<PaginatedResult<GetManuaisAdotadosUoResponse.Manual>> GetManuaisAdotadosUo(GetManuaisAdotadosUoParams filter)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getManuaisAdotadosUo");

            if (_httpContext.User.IsEscolaPrivada())
            {
                request.Headers.Add("cod_uo", "0");
                request.Headers.Add("cod_escola_me", _httpContext.User.GetCodigoServico());
            }
            else
            {
                request.Headers.Add("cod_uo", _httpContext.User.GetCodigoServico());

                if (!string.IsNullOrEmpty(filter.cod_escola_me))
                    request.Headers.Add("cod_escola_me", filter.cod_escola_me);
            }

            request.Headers.Add("utilizador", _httpContext.User.GetUsername());

            if (!string.IsNullOrEmpty(filter.id_ano_letivo))
                request.Headers.Add("id_ano_letivo", filter.id_ano_letivo);

            if (!string.IsNullOrEmpty(filter.id_ano_escolar))
                request.Headers.Add("ano_escolar", filter.id_ano_escolar);

            if (!string.IsNullOrEmpty(filter.id_disciplina))
                request.Headers.Add("id_disciplina", filter.id_disciplina);

            if (!string.IsNullOrEmpty(filter.filtro))
                request.Headers.Add("filtro", filter.filtro);

            if (!string.IsNullOrEmpty(filter.estado_estimativa))
                request.Headers.Add("num_alunos", filter.estado_estimativa);

            if (!string.IsNullOrEmpty(filter.tipologia))
                request.Headers.Add("tipologia", filter.tipologia);

            var response = await SendAsync<GetManuaisAdotadosUoResponse>(request);

            return new PaginatedResult<GetManuaisAdotadosUoResponse.Manual>
            {
                Data = response?.manuais?.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToList() ?? new List<GetManuaisAdotadosUoResponse.Manual>(),
                PageIndex = filter.PageIndex,
                PageSize = filter.PageSize,
                TotalCount = response?.manuais?.Count ?? 0
            };
        }

        public async Task<List<GenericPostResponse.Message>?> SetNumAlunos(SetNumAlunosRequest requestObj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "sime/setNumAlunos");

            if (_httpContext.User.IsEscolaPrivada())
            {
                requestObj.cod_uo = "0";
                requestObj.cod_escola_me = _httpContext.User.GetCodigoServico();
            }
            else
            {
                requestObj.cod_uo = _httpContext.User.GetCodigoServico();
            }

            requestObj.id_ano_letivo = _httpContext.User.GetAnoLetivoSIME();
            requestObj.utilizador = _httpContext.User.GetUsername();

            request.Content = JsonContent.Create(requestObj);

            var response = await SendAsync<GenericPostResponse>(request);

            return response?.messages;
        }
    }
}
