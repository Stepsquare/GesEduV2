using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IServices.SIME;
using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams.SIME;
using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.SIME;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http.Json;

namespace GesEdu.ServiceLayer.Services.SIME
{
    public class PedidosExcecaoServices(
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory,
        IUnitOfWork unitOfWork,
        IHostEnvironment environment) : SIMEBaseServices(httpContextAccessor, httpClientFactory, unitOfWork, environment), IPedidosExcecaoServices
    {
        public async Task<List<GetDisciplinasPLNMResponse.Disciplina>?> GetDisciplinasPLNM(string tipoEnsino)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getDisciplinasPLNM");

            request.Headers.Add("tipo_ensino", tipoEnsino);

            var response = await SendAsync<GetDisciplinasPLNMResponse>(request);

            return response?.disciplinas;
        }

        public async Task<List<GetManuaisEscolaResponseItem>?> GetManuaisEscolaPLNM(string codAgrDisc, string anoEscolar)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getManuaisEscolaPLNM");

            if (_httpContext.User.IsEscolaPrivada())
            {
                request.Headers.Add("cod_uo", "0");
                request.Headers.Add("cod_escola_me", _httpContext.User.GetCodigoServico());
            }
            else
            {
                request.Headers.Add("cod_uo", _httpContext.User.GetCodigoServico());
            }

            request.Headers.Add("utilizador", _httpContext.User.GetUsername());
            request.Headers.Add("id_ano_letivo", _httpContext.User.GetAnoLetivoSIME());
            request.Headers.Add("tipo_acao", "EXE");

            request.Headers.Add("cod_agr_disc", codAgrDisc);
            request.Headers.Add("ano_escolar", anoEscolar);

            return await SendAsync<List<GetManuaisEscolaResponseItem>>(request);
        }


        public async Task<PaginatedResult<GetPedidosExcecaoResponseItem>> GetPedidosExcecao(GetPedidosExcecaoParams filter)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getPedidosExcecao");

            if (_httpContext.User.IsEscolaPrivada())
            {
                request.Headers.Add("cod_uo", "0");
                request.Headers.Add("cod_escola_me", _httpContext.User.GetCodigoServico());
            }
            else
            {
                request.Headers.Add("cod_uo", _httpContext.User.GetCodigoServico());
            }

            request.Headers.Add("utilizador", _httpContext.User.GetUsername());
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

        public async Task<GetPedExcecaoResponse?> GetPedExcecao(string idPedido, string? anoLetivo = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getPedExcecao");

            if (_httpContext.User.IsEscolaPrivada())
            {
                request.Headers.Add("cod_uo", "0");
                request.Headers.Add("cod_escola_me", _httpContext.User.GetCodigoServico());
            }
            else
            {
                request.Headers.Add("cod_uo", _httpContext.User.GetCodigoServico());
            }

            request.Headers.Add("id_ano_letivo", anoLetivo is not null ? anoLetivo : _httpContext.User.GetAnoLetivoSIME());
            request.Headers.Add("id_pedido_cab", idPedido);
            request.Headers.Add("utilizador", _httpContext.User.GetUsername());

            return await SendAsync<GetPedExcecaoResponse>(request);
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

            requestObj.utilizador = _httpContext.User.GetUsername();
            requestObj.id_ano_letivo = _httpContext.User.GetAnoLetivoSIME();

            request.Content = JsonContent.Create(requestObj);

            return await SendAsync<SetPedExcecaoResponse>(request);
        }

        public async Task<List<GenericPostResponse.Message>?> UpdPedExcecao(UpdPedExcecaoRequest requestObj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "sime/updPedExcecao");

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
            requestObj.id_ano_letivo = _httpContext.User.GetAnoLetivoSIME();

            request.Content = JsonContent.Create(requestObj);

            var response = await SendAsync<GenericPostResponse>(request);

            return response?.messages ?? [];
        }

        public async Task<List<GenericPostResponse.Message>?> SetPedEstado(SetPedEstadoRequest requestObj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "sime/setPedEstado");

            requestObj.utilizador = _httpContext.User.GetUsername();
            requestObj.id_ano_letivo = _httpContext.User.GetAnoLetivoSIME();

            request.Content = JsonContent.Create(requestObj);

            var response = await SendAsync<GenericPostResponse>(request);

            return response?.messages ?? [];
        }

        public async Task<List<GenericPostResponse.Message>?> DelManualExcecao(DelManualExcecaoRequest requestObj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "sime/delManualExcecao");

            requestObj.utilizador = _httpContext.User.GetUsername();
            requestObj.id_ano_letivo = _httpContext.User.GetAnoLetivoSIME();

            request.Content = JsonContent.Create(requestObj);

            var response = await SendAsync<GenericPostResponse>(request);

            return response?.messages ?? [];
        }

        public async Task<GetAnexoPedResponse?> GetAnexoPed(string idPedido, string idAnexo, string tipoAnexo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getAnexoPed");

            if (_httpContext.User.IsEscolaPrivada())
            {
                request.Headers.Add("cod_uo", "0");
                request.Headers.Add("cod_escola_me", _httpContext.User.GetCodigoServico());
            }
            else
            {
                request.Headers.Add("cod_uo", _httpContext.User.GetCodigoServico());
            }

            request.Headers.Add("id_pedido_cab", idPedido);
            request.Headers.Add("id_anexo", idAnexo);
            request.Headers.Add("tipo_anexo", tipoAnexo);

            return await SendAsync<GetAnexoPedResponse>(request);
        }

        public async Task<List<GenericPostResponse.Message>?> SetAnexoPed(IFormFile file, int idPedido, int tipoAnexo)
        {
            var requestObj = new SetAnexoPedRequest
            {
                id_pedido_cab = idPedido,
                tipo_anexo = tipoAnexo,
                filename = file.FileName,
                mimetype = file.ContentType,
                utilizador = _httpContext.User.GetUsername()
            };

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                requestObj.anexo_base64 = Convert.ToBase64String(fileBytes);
            }

            var request = new HttpRequestMessage(HttpMethod.Post, "sime/setAnexoPed");

            requestObj.utilizador = _httpContext.User.GetUsername();

            request.Content = JsonContent.Create(requestObj);

            var response = await SendAsync<GenericPostResponse>(request);

            return response?.messages ?? [];
        }

        public async Task<List<GenericPostResponse.Message>?> DelAnexoPed(int idPedido, int idAnexo, int tipoAnexo)
        {
            var requestObj = new DelAnexoPedRequest
            {
                id_pedido_cab = idPedido,
                id_anexo = idAnexo,
                tipo_anexo = tipoAnexo,
                utilizador = _httpContext.User.GetUsername()
            };
            
            var request = new HttpRequestMessage(HttpMethod.Post, "sime/delAnexoPed");

            request.Content = JsonContent.Create(requestObj);

            var response = await SendAsync<GenericPostResponse>(request);

            return response?.messages ?? [];
        }
    }
}
