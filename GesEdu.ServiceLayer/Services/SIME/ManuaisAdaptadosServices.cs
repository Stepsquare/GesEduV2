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
    public class ManuaisAdaptadosServices(
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory,
        IUnitOfWork unitOfWork,
        IHostEnvironment environment) : SIMEBaseServices(httpContextAccessor, httpClientFactory, unitOfWork, environment), IManuaisAdaptadosServices
    {
        public async Task<PaginatedResult<GetAlunosManuaisAdaptadosResponse.AlunoManuaisAdaptados>> GetAlunosManuaisAdaptados(GetAlunosManuaisAdaptadosParams filter)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getAlunosManuaisAdaptados");

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

            if (!string.IsNullOrEmpty(filter.TipoDoc))
                request.Headers.Add("tipo_doc_aluno", filter.TipoDoc);

            if (!string.IsNullOrEmpty(filter.NumDoc))
                request.Headers.Add("num_doc_aluno", filter.NumDoc);

            if (!string.IsNullOrEmpty(filter.Nome))
                request.Headers.Add("nome_aluno", filter.Nome);

            var response = await SendAsync<GetAlunosManuaisAdaptadosResponse>(request);

            return new PaginatedResult<GetAlunosManuaisAdaptadosResponse.AlunoManuaisAdaptados>
            {
                Data = response?.alunos_manuais_adaptados?.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToList() ?? new List<GetAlunosManuaisAdaptadosResponse.AlunoManuaisAdaptados>(),
                PageIndex = filter.PageIndex,
                PageSize = filter.PageSize,
                TotalCount = response?.alunos_manuais_adaptados?.Count ?? 0
            };
        }

        public async Task<GetDetAlunosManAdaptadosResponse?> GetDetAlunosManAdaptados(int id_aluno)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getManuaisAprDet");

            request.Headers.Add("id_ano_letivo", _httpContext.User.GetAnoLetivoSIME());
            request.Headers.Add("id_aluno", id_aluno.ToString());

            return await SendAsync<GetDetAlunosManAdaptadosResponse>(request);
        }

        public async Task<GetIdentificaAlunoManAdaptResponse?> GetIdentificaAlunoManAdapt(string tipoDoc, string numDoc)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getIdentificaAlunoManAdapt");

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
            request.Headers.Add("tipo_doc_aluno", tipoDoc);
            request.Headers.Add("num_doc_aluno", numDoc);

            return await SendAsync<GetIdentificaAlunoManAdaptResponse>(request);
        }

        public async Task<GetManuaisAnoEscolarResponse?> GetManuaisAnoEscolar(int idAluno, int anoEscolar)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getManuaisAnoEscolar");

            request.Headers.Add("id_ano_letivo", _httpContext.User.GetAnoLetivoSIME());
            request.Headers.Add("id_aluno", idAluno.ToString());
            request.Headers.Add("ano_escolar", anoEscolar.ToString());

            return await SendAsync<GetManuaisAnoEscolarResponse>(request);
        }

        public async Task<List<GenericPostResponse.Message>?> SetAlunosManuaisAdaptados(SetAlunosManuaisAdaptadosRequest requestObj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "sime/setAlunosManuaisAdaptados");

            requestObj.id_ano_letivo = _httpContext.User.GetAnoLetivoSIME();
            requestObj.utilizador = _httpContext.User.GetUsername();

            request.Content = JsonContent.Create(requestObj);

            var response = await SendAsync<GenericPostResponse>(request);

            return response?.messages;
        }
    }
}
