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
    public class ApreciacaoManuaisServices(
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory,
        IUnitOfWork unitOfWork,
        IHostEnvironment environment) : SIMEBaseServices(httpContextAccessor, httpClientFactory, unitOfWork, environment), IApreciacaoManuaisServices
    {
        public async Task<PaginatedResult<GetManuaisApreciadosResponseItem>> GetManuaisApreciados(GetManuaisApreciadosParams filter)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getManuaisApreciados");

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

            if (!string.IsNullOrEmpty(filter.certificado))
                request.Headers.Add("certificado", filter.certificado);

            if (!string.IsNullOrEmpty(filter.custom))
                request.Headers.Add("custom", filter.custom);

            if (!string.IsNullOrEmpty(filter.id_ano_letivo))
                request.Headers.Add("id_ano_letivo", filter.id_ano_letivo);

            if (!string.IsNullOrEmpty(filter.ano_escolar))
                request.Headers.Add("ano_escolar", filter.ano_escolar);

            if (!string.IsNullOrEmpty(filter.id_disciplina))
                request.Headers.Add("id_disciplina", filter.id_disciplina);

            if (!string.IsNullOrEmpty(filter.estado))
                request.Headers.Add("estado", filter.estado);

            if (!string.IsNullOrEmpty(filter.tipologia))
                request.Headers.Add("tipologia", filter.tipologia);

            var response = await SendAsync<List<GetManuaisApreciadosResponseItem>>(request);

            return new PaginatedResult<GetManuaisApreciadosResponseItem>
            {
                Data = response?.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToList() ?? new List<GetManuaisApreciadosResponseItem>(),
                PageIndex = filter.PageIndex,
                PageSize = filter.PageSize,
                TotalCount = response?.Count ?? 0
            };
        }

        public async Task<GetManuaisAprDetResponse?> GetManualApreciado(string id_manual, string id_ano_letivo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getManuaisAprDet");

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

            request.Headers.Add("id_ano_letivo", id_ano_letivo);
            request.Headers.Add("id_manual", id_manual);

            return await SendAsync<GetManuaisAprDetResponse>(request);
        }

        public async Task<GetManuaisSIMEResponse?> GetManuaisSIMEPdfExport(string ano_letivo, string ciclo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getManuaisSIME");

            request.Headers.Add("utilizador", _httpContext.User.GetUsername());
            request.Headers.Add("id_ano_letivo", ano_letivo);
            request.Headers.Add("ciclo", ciclo);

            return await SendAsync<GetManuaisSIMEResponse>(request);
        }

        public async Task<List<GenericPostResponse.Message>?> SetManualApreciado(SetManualAprDetRequest requestObj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "sime/setManualAprDet");

            if (_httpContext.User.IsEscolaPrivada())
            {
                requestObj.cod_uo = "0";
                requestObj.cod_escola_me = _httpContext.User.GetCodigoServico();
            }
            else
            {
                requestObj.cod_uo = _httpContext.User.GetCodigoServico();
            }

            requestObj.utilizador = _httpContext.User.GetUsername();

            request.Content = JsonContent.Create(requestObj);

            var response = await SendAsync<GenericPostResponse>(request);

            return response?.messages;
        }
    }
}
