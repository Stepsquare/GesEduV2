using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IServices.AreaReservada;
using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams.AreaReservada;
using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.AreaReservada.Cadastro;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;

namespace GesEdu.ServiceLayer.Services.AreaReservada
{
    public class TrabalhadoresServices(
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory,
        IUnitOfWork unitOfWork,
        IHostEnvironment environment) : BaseServices(httpContextAccessor, httpClientFactory, unitOfWork, environment), ITrabalhadoresServices
    {

        public async Task<PaginatedResult<GetFuncionariosUoRespResponse.Funcionario>> GetFuncionariosUoResp(GetFuncionariosUoRespParams filter)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "cadastro/getFuncionariosUoResp");

            request.Headers.Add("uo", _httpContext.User.GetCodigoServico());

            if (!string.IsNullOrEmpty(filter.nif_pes))
                request.Headers.Add("nif_pes", filter.nif_pes);

            if (!string.IsNullOrEmpty(filter.regime))
                request.Headers.Add("regime", filter.regime);

            var response = await SendAsync<GetFuncionariosUoRespResponse>(request);

            return new PaginatedResult<GetFuncionariosUoRespResponse.Funcionario>
            {
                Data = response?.funcionarios?.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToList() ?? new List<GetFuncionariosUoRespResponse.Funcionario>(),
                PageIndex = filter.PageIndex,
                PageSize = filter.PageSize,
                TotalCount = response?.funcionarios?.Count ?? 0
            };
        }

        public async Task<GetFuncionariosUoRespResponse.Funcionario?> GetFuncionarioUoResp(string nif)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "cadastro/getFuncionariosUoResp");

            request.Headers.Add("uo", _httpContext.User.GetCodigoServico());
            request.Headers.Add("nif_pes", nif);

            var response = await SendAsync<GetFuncionariosUoRespResponse>(request);

            return response?.funcionarios.FirstOrDefault();
        }

        public async Task<List<GetDocenteHabilitacaoResponseItem>?> GetDocenteHabilitacao(string id_pessoa_unica)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "cadastro/getDocentesHabilitacao");

            request.Headers.Add("uo", _httpContext.User.GetCodigoServico());
            request.Headers.Add("nif_agr", _httpContext.User.GetNifServico());
            request.Headers.Add("id_pessoa_unica", id_pessoa_unica);

            return await SendAsync<List<GetDocenteHabilitacaoResponseItem>>(request);
        }

        public async Task<GetDocenteHabilitacaoResponseItem?> GetDocenteHabilitacao(string id_pessoa_unica, int id_habilitacao)
        {
            var habilitacoes = await GetDocenteHabilitacao(id_pessoa_unica);

            return habilitacoes?.FirstOrDefault(x => x.id_habilitacao == id_habilitacao);
        }

        public async Task<List<GetDocenteFormacaoResponseItem>?> GetDocenteFormacao(string id_pessoa_unica)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "cadastro/getDocentesFormacao");

            request.Headers.Add("uo", _httpContext.User.GetCodigoServico());
            request.Headers.Add("nif_agr", _httpContext.User.GetNifServico());
            request.Headers.Add("id_pessoa_unica", id_pessoa_unica);

            return await SendAsync<List<GetDocenteFormacaoResponseItem>>(request);
        }

        public async Task<GetDocenteFormacaoResponseItem?> GetDocenteFormacao(string id_pessoa_unica, int id_formacao)
        {
            var formacoes = await GetDocenteFormacao(id_pessoa_unica);

            return formacoes?.FirstOrDefault(x => x.id_formacao == id_formacao);
        }

        public async Task<GetAnexoPessoaResponse?> GetAnexoPessoa(string id_anexo_pessoa)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "cadastro/getAnexoPessoa");

            request.Headers.Add("id_anexo_pessoa", id_anexo_pessoa);

            return await SendAsync<GetAnexoPessoaResponse>(request);
        }

        public async Task<List<GenericPostResponse.Message>?> SetDocenteEstadoHabilitacao(SetDocenteEstadoHabilitacaoRequest requestObj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "cadastro/setDocenteEstadoHabilitacao");

            request.Headers.Add("utz_login", _httpContext.User.GetUsername());

            request.Content = JsonContent.Create(requestObj);

            var response = await SendAsync<GenericPostResponse>(request);

            return response?.messages;
        }

        public async Task<List<GenericPostResponse.Message>?> SetDocenteEstadoFormacao(SetDocenteEstadoFormacaoRequest requestObj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "cadastro/setDocenteEstadoFormacao");

            request.Headers.Add("utz_login", _httpContext.User.GetUsername());

            request.Content = JsonContent.Create(requestObj);

            var response = await SendAsync<GenericPostResponse>(request);

            return response?.messages;
        }
    }
}
