using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IHelpers;
using GesEdu.Shared.Interfaces.ISevices;
using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams;
using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.Auth;
using Microsoft.AspNetCore.Http;

namespace GesEdu.ServiceLayer.Services
{
    public class UserServices : BaseServices, IUserServices
    {
        private readonly HttpContext _httpContext;

        public UserServices(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IGenericRestRequests genericRestRequests) : base(unitOfWork, genericRestRequests)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<GetUtilizadorResponse?> GetUtilizador(int userId)
        {
            var headers = new Dictionary<string, string>
            {
                { "id_utilizador", userId.ToString() }
            };

            return await _genericRestRequests.Get<GetUtilizadorResponse>("auth", "getUtilizador", headers);
        }

        public async Task<PaginatedResult<GetUtilizadoresResponseItem>> GetUtilizadores(GetUtilizadoresParams searchParams)
        {
            var headers = new Dictionary<string, string>
            {
                //TODO - Rever se é possivel o getUtilizadores ter como parametro o cod_serviço em vez do id_serviço
                { "id_servico", _httpContext.User.GetIdServico() }
            };

            var getUtilizadoresResponse = await _genericRestRequests.Get<List<GetUtilizadoresResponseItem>>("auth", "getUtilizadores", headers);

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
            var headers = new Dictionary<string, string>
            {
                { "nome", "APP_EXT_GES_EDU" }
            };

            return await _genericRestRequests.Get<List<GetPerfisAppResponseItem>>("auth", "getPerfisApp", headers!);
        }

        public async Task<string?> CriarUtilizador(string nome, string email, string password, IDictionary<int, bool> profiles)
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
                perfis = profiles.Where(x => x.Value).Select(x => new CriarUtilizadorRequest.perfil
                {
                    id_perfil = x.Key,
                    acao = x.Value ? "A" : "R"
                }).ToList()
            };

            var criarUtilizadorResponse = await _genericRestRequests.Post<GenericPostResponse, CriarUtilizadorRequest>("auth", "criarUtilizador", requestBody);

            return criarUtilizadorResponse?.messages.FirstOrDefault()?.msg;
        }

        public async Task<string?> AlterarPerfilUtilizador(int userId, int profileId, bool isChecked)
        {
            var requestBody = new AlterarPerfilUtzRequest
            {
                id_utilizador = userId,
                perfis = new List<AlterarPerfilUtzRequest.perfil>()
                {
                    new AlterarPerfilUtzRequest.perfil
                    {
                        id_perfil = profileId,
                        acao = isChecked ? "A" : "R"
                    }
                }
            };

            var alterarPerfilUtzResponse = await _genericRestRequests.Post<GenericPostResponse, AlterarPerfilUtzRequest>("auth", "alterarPerfilUtz", requestBody);

            return alterarPerfilUtzResponse?.messages.FirstOrDefault()?.msg;
        }

        public async Task<string?> AlterarEstadoUtilizador(int userId, bool isActive)
        {
            var requestBody = new AlterarEstadoUtilizadorRequest
            {
                id_utilizador = userId,
                estado = isActive ? "A" : "C",
                id_servico = _httpContext.User.GetIdServico()
            };

            var alterarEstadoUtilizadorResponse = await _genericRestRequests.Post<GenericPostResponse, AlterarEstadoUtilizadorRequest>("auth", "alterarEstadoUtilizador", requestBody);

            return alterarEstadoUtilizadorResponse?.messages.FirstOrDefault()?.msg;
        }
    }
}
