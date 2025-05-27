using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IServices.SIME;
using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.SIME;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;

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

        public async Task<GetManuaisAdotadosResponse.GetManuaisAdotadosObject?> GetManuaisAdotados(string ciclo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getManuaisAdotados");

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
            request.Headers.Add("ciclo", ciclo);

            var response = await SendAsync<GetManuaisAdotadosResponse>(request);

            return response?.manuais_adotados;
        }

        public async Task<List<GenericPostResponse.Message>?> SetManuaisEscola(SetManuaisEscolaRequest requestObj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "sime/setManuaisEscola");

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

            var response = await SendAsync<GenericPostResponse>(request);

            return response?.messages;
        }

        public async Task<List<GenericPostResponse.Message>?> DelManuaisEscola(int id_manual)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "sime/delManuaisEscola");

            var requestObj = new DelManuaisEscolaRequest
            {
                id_manual = id_manual,
                id_ano_letivo = _httpContext.User.GetAnoLetivoSIME(),
                utilizador = _httpContext.User.GetCodigoServico(),
                cod_uo = _httpContext.User.IsEscolaPrivada() ? "0" : _httpContext.User.GetCodigoServico(),
                cod_escola_me = _httpContext.User.IsEscolaPrivada() ? _httpContext.User.GetCodigoServico() : null,
            };

            request.Content = JsonContent.Create(requestObj);

            var response = await SendAsync<GenericPostResponse>(request);

            return response?.messages;
        }
    }
}
