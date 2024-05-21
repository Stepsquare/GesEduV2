using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IHelpers;
using GesEdu.Shared.Interfaces.ISevices;
using GesEdu.Shared.WebserviceModels.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using GesEdu.Shared.WebserviceModels.Manuais;
using GesEdu.Shared.ExceptionHandler.Exceptions;
using System.Net;

namespace GesEdu.ServiceLayer.Services
{
    public class LoginServices : BaseServices, ILoginServices
    {
        private readonly HttpContext _httpContext;

        public LoginServices(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IGenericRestRequests genericRestRequests) : base(unitOfWork, genericRestRequests)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<(bool IsAdmin, bool ChangePassword)> SignIn(string username, string password)
        {
            bool isAdmin = false;
            bool changePassword = false;

            var obj = new LoginUtilizadorRequest
            {
                username = username,
                password = password
            };

            var loginUtilizadorResponse = await _genericRestRequests.Post<LoginUtilizadorResponse, LoginUtilizadorRequest>("auth", "loginUtilizador", obj);

            //TODO - Rever validação de entrada no GesEDU... Perfil generico de entrada? Ou manter como está...
            if (loginUtilizadorResponse!.cod_origem == "PUB"
                    || !loginUtilizadorResponse.perfis.Any(x => x.cod_perfil!.Contains("APP_EXTERNA_GES_EDU")))
                throw new WebserviceException(HttpStatusCode.Unauthorized, "Utilizador não sem perfil de acesso.");

            var getFaseResponse = await _genericRestRequests.Get<GetFaseResponse>("manuais", "getFase");

            var claims = new List<Claim>
                {
                    //Dados User
                    new Claim(ClaimTypes.Name, loginUtilizadorResponse.nome!),
                    new Claim(ClaimTypes.NameIdentifier, username),
                    new Claim(ClaimTypes.Sid, loginUtilizadorResponse.id_utilizador!),

                    //Claims do serviço getFase
                    new Claim("ANO_LETIVO", getFaseResponse!.id_ano_letivo_atual!),
                    new Claim("DES_ANO_LETIVO", getFaseResponse.des_id_ano_letivo_atual!),
                    new Claim("ANO_LETIVO_ANTERIOR", getFaseResponse.id_ano_letivo_anterior!),
                    new Claim("DES_ANO_LETIVO_ANTERIOR", getFaseResponse.des_id_ano_letivo_anterior!),
                    new Claim("ESTADO_FASE", getFaseResponse.cod_estado_fase!),
                    new Claim("COD_ORIGEM", loginUtilizadorResponse.cod_origem!),
                    new Claim("NOME_ORIGEM", loginUtilizadorResponse.nome_origem!),
            };

            //TODO - Descomentar validação de Perfil ADMIN
            if (true || loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == "APP_EXTERNA_GES_EDU_ADMIN"))
            {
                isAdmin = true;
                claims.Add(new Claim(ClaimTypes.Role, "ADMIN"));

                //TODO - Verificar o registo de id_serviço nas claims
                claims.Add(new Claim("ID_SERVICO", loginUtilizadorResponse.id_servico!));
            }
            else
            {
                claims.Add(new Claim("ID_SERVICO", loginUtilizadorResponse.id_servico!));
                claims.Add(new Claim("COD_SERVICO", loginUtilizadorResponse.cod_servico!));
                claims.Add(new Claim("NOME_SERVICO", loginUtilizadorResponse.nome_servico!));
                claims.Add(new Claim("NIF_SERVICO", loginUtilizadorResponse.nif_servico!));

                //Perfil de gestão de Utilizadores
                if (loginUtilizadorResponse.responsavel == "S")
                    claims.Add(new Claim(ClaimTypes.Role, "USER_MANAGER"));

                //Perfil MEGA
                if (loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == "APP_EXTERNA_GES_EDU_MEGA"))
                    claims.Add(new Claim(ClaimTypes.Role, "MEGA_USER"));

                //Perfil Area Reservada
                if (loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == "APP_EXTERNA_GES_EDU_AREA_RESERVADA"))
                {
                    claims.Add(new Claim(ClaimTypes.Role, "AREA_RESERVADA_USER"));

                    if (loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == "APP_EXTERNA_GES_EDU_REACT"))
                        claims.Add(new Claim(ClaimTypes.Role, "AREA_RESERVADA_REACT_USER"));
                }

                //Perfil SIME para DGE
                if (loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == "APP_EXTERNA_GES_EDU_SIME_DGE"))
                    claims.Add(new Claim(ClaimTypes.Role, "SIME_DGE_USER"));

                //Perfil SIME para Escolas
                if (loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == "APP_EXTERNA_GES_EDU_SIME_ESC"))
                    claims.Add(new Claim(ClaimTypes.Role, "SIME_ESC_USER"));
            }

            changePassword = loginUtilizadorResponse.trocar_password == "S";

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return (isAdmin, changePassword);
        }

        public async Task SignOut()
        {
            await _httpContext.SignOutAsync();
        }

        public async Task<string?> PasswordRecovery(string? email)
        {
            var obj = new ResetPasswordRequest
            {
                username = email,
                email = email
            };

            var resetPasswordResponse = await _genericRestRequests.Post<ResetPasswordResponse, ResetPasswordRequest>("auth", "resetPassword", obj);

            return resetPasswordResponse?.messages.FirstOrDefault()?.msg;
        }

        public async Task<string?> PasswordChange(string? username, string? oldPassword, string? newPassword)
        {
            var obj = new AlterarPasswordRequest
            {
                username = username,
                current_password = oldPassword,
                new_password = newPassword
            };

            var alterarPasswordResponse = await _genericRestRequests.Post<AlterarPasswordResponse, AlterarPasswordRequest>("auth", "alterarPassword", obj);

            return alterarPasswordResponse?.messages.FirstOrDefault()?.msg;
        }

        public async Task<List<GetUoResponseItem>?> GetUo(string? idServico)
        {
            var headers = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(idServico))
                headers.Add("Id_servico", idServico);

            var GetUoResponse = await _genericRestRequests.Get<List<GetUoResponseItem>>("manuais", "getUO", headers);

            return GetUoResponse;
        }

        public async Task SetUo(GetUoResponseItem model, ClaimsPrincipal principal)
        {
            var clone = principal.Clone();
            var newIdentity = clone.Identity as ClaimsIdentity;

            var claimsToRemove = newIdentity?.FindAll(x => x.Type == "COD_SERVICO" || x.Type == "NOME_SERVICO" || x.Type == "NIF_SERVICO").ToList();

            foreach (var claim in claimsToRemove!)
            {
                newIdentity!.RemoveClaim(claim);
            }

            newIdentity?.AddClaims(new List<Claim> {
                //TODO - Acrescentar id_serviço ao serviço getUo para alterar o campo ao alterar o contexto da UO
                //new Claim("ID_SERVICO", "model.id_servico!"),
                new Claim("COD_SERVICO", model.Cod_agrupamento!),
                new Claim("NOME_SERVICO", model.Nome!),
                new Claim("NIF_SERVICO", model.Nif_servico.ToString())
            });

            await _httpContext.SignOutAsync();

            await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(newIdentity!));
        }
    }
}
