using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IServices.SIME;
using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams.SIME;
using GesEdu.Shared.WebserviceModels.SIME;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;

namespace GesEdu.ServiceLayer.Services.SIME
{
    public class PedidosExcecaoServices(
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory,
        IUnitOfWork unitOfWork,
        IHostEnvironment environment) : SIMEBaseServices(httpContextAccessor, httpClientFactory, unitOfWork, environment), IPedidosExcecaoServices
    {
        public async Task<List<GetDisciplinasPLNMResponse.Disciplina>> GetDisciplinasPLNM(string tipoEnsino)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getDisciplinasPLNM");

            request.Headers.Add("tipo_ensino", tipoEnsino);

            var response = await SendAsync<GetDisciplinasPLNMResponse>(request);

            return response?.disciplinas ?? [];
        }

        public async Task<PaginatedResult<GetPedidosExcecaoResponseItem>> GetPedidosExcecao(GetPedidosExcecaoParams filter)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getPedidosExcecao");

            request.Headers.Add("utilizador", _httpContext.User.GetUsername());

            if (_httpContext.User.IsEscolaPrivada())
            {
                request.Headers.Add("cod_uo", "0");
                request.Headers.Add("cod_escola_me", _httpContext.User.GetCodigoServico());
            }
            else
            {
                request.Headers.Add("cod_uo", _httpContext.User.GetCodigoServico());
            }

            request.Headers.Add("id_ano_letivo", filter.ano_letivo);

            if (!string.IsNullOrEmpty(filter.ano_escolar))
                request.Headers.Add("ano_escolar", filter.ano_escolar);

            if (!string.IsNullOrEmpty(filter.id_disciplina))
                request.Headers.Add("id_disciplina", filter.id_disciplina);

            if (!string.IsNullOrEmpty(filter.tipo_excecao))
                request.Headers.Add("id_excecao", filter.tipo_excecao);

            if (!string.IsNullOrEmpty(filter.estado_excecao))
                request.Headers.Add("estado", filter.estado_excecao);

            var response = await SendAsync<List<GetPedidosExcecaoResponseItem>>(request);

            return new PaginatedResult<GetPedidosExcecaoResponseItem>
            {
                Data = response?.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToList() ?? new List<GetPedidosExcecaoResponseItem>(),
                PageIndex = filter.PageIndex,
                PageSize = filter.PageSize,
                TotalCount = response?.Count ?? 0
            };
        }

        public async Task<SetPedExcecaoResponse?> SetPedExcecao(SetPedExcecaoRequest requestObj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "sime/setPedExcecao");

            if (_httpContext.User.IsEscolaPrivada())
            {
                requestObj.cod_uo = "0";
                requestObj.cod_escola_me = _httpContext.User.GetCodigoServico();
            }
            else
            {
                requestObj.cod_uo = _httpContext.User.GetCodigoServico();
            }

            requestObj.utilizador = _httpContext.User.GetCodigoServico();
            requestObj.id_ano_letivo = _httpContext.User.GetAnoLetivoSIME();

            request.Content = JsonContent.Create(requestObj);

            return await SendAsync<SetPedExcecaoResponse>(request);
        }
    }
}
