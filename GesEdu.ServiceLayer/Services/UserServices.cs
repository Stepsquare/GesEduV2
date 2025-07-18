﻿using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IServices;
using GesEdu.Shared.Pagination;
using GesEdu.Shared.Resources;
using GesEdu.Shared.SearchParams;
using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;

namespace GesEdu.ServiceLayer.Services
{
    public class UserServices(
        IHttpContextAccessor httpContextAccessor, 
        IHttpClientFactory httpClientFactory, 
        IUnitOfWork unitOfWork, 
        IHostEnvironment environment) : BaseServices(httpContextAccessor, httpClientFactory, unitOfWork, environment), IUserServices
    {
        public async Task<GetUtilizadorResponse?> GetUtilizador(int userId)
        {
            var getUtilizadorRequest = new HttpRequestMessage(HttpMethod.Get, "auth/getUtilizador");

            getUtilizadorRequest.Headers.Add("id_utilizador", userId.ToString());

            return await SendAsync<GetUtilizadorResponse>(getUtilizadorRequest);
        }

        public async Task<PaginatedResult<GetUtilizadoresResponseItem>> GetUtilizadores(GetUtilizadoresParams searchParams)
        {

            var getUtilizadoresRequest = new HttpRequestMessage(HttpMethod.Get, "auth/getUtilizadores");

            getUtilizadoresRequest.Headers.Add("id_servico", _httpContext.User.IsAdmin() && searchParams.IsIgefeUsers ? _httpContext.User.GetIdServicoOrigem() : _httpContext.User.GetIdServico());

            var getUtilizadoresResponse = await SendAsync<List<GetUtilizadoresResponseItem>>(getUtilizadoresRequest);

            return new PaginatedResult<GetUtilizadoresResponseItem>
            {
                Data = getUtilizadoresResponse!
                        .Where(x => x.ID_UTILIZADOR.ToString() != _httpContext.User.GetUserId()
                            && (!searchParams.IsActive.HasValue
                                || (searchParams.IsActive.Value && x.ESTADO == "A")
                                || (!searchParams.IsActive.Value && x.ESTADO != "A"))
                            && (string.IsNullOrEmpty(searchParams.Username)
                                || x.EMAIL!.ToLower().Contains(searchParams.Username.ToLower())
                                || x.NOME!.ToLower().Contains(searchParams.Username.ToLower())))
                        .Skip((searchParams.PageIndex - 1) * searchParams.PageSize).Take(searchParams.PageSize)
                        .ToList(),
                PageIndex = searchParams.PageIndex,
                PageSize = searchParams.PageSize,
                TotalCount = getUtilizadoresResponse!
                                .Count(x => x.ID_UTILIZADOR.ToString() != _httpContext.User.GetUserId()
                                    && (!searchParams.IsActive.HasValue
                                        || (searchParams.IsActive.Value && x.ESTADO == "A")
                                        || (!searchParams.IsActive.Value && x.ESTADO != "A"))
                                    && (string.IsNullOrEmpty(searchParams.Username)
                                        || x.EMAIL!.ToLower().Contains(searchParams.Username.ToLower())
                                        || x.NOME!.ToLower().Contains(searchParams.Username.ToLower())))
            };
        }

        public async Task<List<GetPerfisAppResponseItem>?> GetPerfis()
        {
            var getPerfisRequest = new HttpRequestMessage(HttpMethod.Get, "auth/getPerfisApp");

            getPerfisRequest.Headers.Add("nome", GesEduProfiles.CODE);

            return await SendAsync<List<GetPerfisAppResponseItem>>(getPerfisRequest);
        }

        public async Task<List<GenericPostResponse.Message>?> CriarUtilizador(string nome, string email, string password, IDictionary<int, bool> profiles)
        {
            var requestBody = new CriarUtilizadorRequest
            {
                nome = nome,
                email = email,
                username = email,
                password = password,
                dt_inicial = DateTime.Now.ToString("dd-MM-yyyy"),
                servico = _httpContext.User.GetCodigoServico(),
                responsavel = "N",
                perfis = profiles.Where(x => x.Value).Select(x => new CriarUtilizadorRequest.Perfil
                {
                    id_perfil = x.Key,
                    acao = x.Value ? "A" : "R"
                }).ToList()
            };

            var criarUtilizadorRequest = new HttpRequestMessage(HttpMethod.Post, "auth/criarUtilizador");

            criarUtilizadorRequest.Content = JsonContent.Create(requestBody);

            var criarUtilizadorResponse = await SendAsync<GenericPostResponse>(criarUtilizadorRequest);

            return criarUtilizadorResponse?.messages;
        }

        public async Task<List<GenericPostResponse.Message>?> AlterarPerfilUtilizador(int userId, int profileId, bool isChecked)
        {
            var requestBody = new AlterarPerfilUtzRequest
            {
                id_utilizador = userId,
                perfis = new List<AlterarPerfilUtzRequest.Perfil>()
                {
                    new AlterarPerfilUtzRequest.Perfil
                    {
                        id_perfil = profileId,
                        acao = isChecked ? "A" : "R"
                    }
                }
            };

            var alterarPerfilUtzRequest = new HttpRequestMessage(HttpMethod.Post, "auth/alterarPerfilUtz");

            alterarPerfilUtzRequest.Content = JsonContent.Create(requestBody);

            var alterarPerfilUtzResponse = await SendAsync<GenericPostResponse>(alterarPerfilUtzRequest);


            return alterarPerfilUtzResponse?.messages;
        }

        public async Task<List<GenericPostResponse.Message>?> AlterarEstadoUtilizador(int userId, bool isActive, bool isIgefeUser)
        {
            var requestBody = new AlterarEstadoUtilizadorRequest
            {
                id_utilizador = userId,
                estado = isActive ? "A" : "C",
                id_servico = isIgefeUser ? _httpContext.User.GetIdServicoOrigem() : _httpContext.User.GetIdServico()
            };

            var alterarEstadoUtilizadorRequest = new HttpRequestMessage(HttpMethod.Post, "auth/alterarEstadoUtilizador");

            alterarEstadoUtilizadorRequest.Content = JsonContent.Create(requestBody);

            var alterarEstadoUtilizadorResponse = await SendAsync<GenericPostResponse>(alterarEstadoUtilizadorRequest);

            return alterarEstadoUtilizadorResponse?.messages;
        }
    }
}
