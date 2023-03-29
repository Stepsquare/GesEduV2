using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IHelpers;
using GesEdu.Shared.Interfaces.ISevices;
using GesEdu.Shared.WebserviceModels.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;
using static GesEdu.Shared.WebserviceModels.GenericPostResponse;
using GesEdu.Shared.WebserviceModels.Manuais;
using System.Collections;
using System.Security.Principal;

namespace GesEdu.ServiceLayer.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly HttpContext _httpContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRestRequests _genericRestRequests;

        public LoginServices(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IGenericRestRequests genericRestRequests)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _unitOfWork = unitOfWork;
            _genericRestRequests = genericRestRequests;
        }

        public async Task<(bool IsLoginSuccessful, bool IsAdmin, bool ChangePassword, string? ErrorMessage)> SignIn(string username, string password)
        {
            bool isLoginSuccessful = false;
            bool isAdmin = false;
            bool changePassword = false;

            var obj = new LoginUtilizadorRequest
            {
                username = username,
                password = password
            };

            var loginUtilizadorResult = await _genericRestRequests.Post<LoginUtilizadorResponse, LoginUtilizadorRequest>("auth", "loginUtilizador", obj);

            //Verificar se webservice tem retorno e se a resposta tem httpCode 200
            if (loginUtilizadorResult == null || !loginUtilizadorResult.messages.Any(x => x.cod_msg == "200"))
                return (isLoginSuccessful, isAdmin, changePassword, loginUtilizadorResult != null ? loginUtilizadorResult.messages.FirstOrDefault()?.msg : "Erro no webservice de Login. Contactar suporte.");

            var getFaseResult = await _genericRestRequests.Get<GetFaseResponse>("manuais", "getFase");

            if (getFaseResult == null)
                return (isLoginSuccessful, isAdmin, changePassword, "Erro ao retornar a fase da aplicação. Contacte o suporte.");


            //Verificar os perfis do GesEDU e o cod_origem
            if (loginUtilizadorResult.cod_origem != "PUB"
                && loginUtilizadorResult.perfis.Any(x => x.cod_perfil == "APP_EXTERNA_GES_EDU"))
            {
                isLoginSuccessful = true;
                changePassword = loginUtilizadorResult.trocar_password == "S";

                var claims = new List<Claim>
                {
                    //Dados User
                    new Claim(ClaimTypes.Name, loginUtilizadorResult.nome),
                    new Claim(ClaimTypes.NameIdentifier, username),
                    new Claim(ClaimTypes.Sid, loginUtilizadorResult.id_utilizador),

                    //Claims do serviço getFase
                    new Claim("ANO_LETIVO", getFaseResult.id_ano_letivo_atual),
                    new Claim("DES_ANO_LETIVO", getFaseResult.des_id_ano_letivo_atual),
                    new Claim("ANO_LETIVO_ANTERIOR", getFaseResult.id_ano_letivo_anterior),
                    new Claim("DES_ANO_LETIVO_ANTERIOR", getFaseResult.des_id_ano_letivo_anterior),
                    new Claim("ESTADO_FASE", getFaseResult.cod_estado_fase),
                    new Claim("COD_ORIGEM", loginUtilizadorResult.cod_origem),
                    new Claim("NOME_ORIGEM", loginUtilizadorResult.nome_origem),
                    new Claim("ID_SERVICO", loginUtilizadorResult.id_servico)
                };

                //Perfis de utilizador
                var PerfilGesedu = loginUtilizadorResult.perfis.FirstOrDefault(x => x.cod_perfil == "APP_EXTERNA_GES_EDU");

                if (PerfilGesedu != null)
                {
                    if (true || PerfilGesedu.objetos.Any(x => x.codigo == "ADMIN"))
                    {
                        isAdmin = true;
                        claims.Add(new Claim(ClaimTypes.Role, "ADMIN"));
                    }
                    else
                    {
                        claims.Add(new Claim("COD_SERVICO", loginUtilizadorResult.cod_servico));
                        claims.Add(new Claim("NOME_SERVICO", loginUtilizadorResult.nome_servico));
                        claims.Add(new Claim("NIF_SERVICO", loginUtilizadorResult.nif_servico));

                        if (loginUtilizadorResult.responsavel == "S")
                            claims.Add(new Claim(ClaimTypes.Role, "USER_MANAGER"));

                        var perfilMega = PerfilGesedu.objetos.FirstOrDefault(x => x.codigo == "MEGA");
                        if (perfilMega != null)
                        {
                            if (perfilMega.ler == "1")
                                claims.Add(new Claim(ClaimTypes.Role, "USER_MEGA_READ"));
                            if (perfilMega.escrever == "1")
                                claims.Add(new Claim(ClaimTypes.Role, "USER_MEGA_WRITE"));
                        }

                        var perfilAreaRsv = PerfilGesedu.objetos.FirstOrDefault(x => x.codigo == "AREA_RESERVADA");
                        if (perfilAreaRsv != null)
                        {
                            if (perfilAreaRsv.ler == "1")
                                claims.Add(new Claim(ClaimTypes.Role, "USER_AREA_RSV"));
                            if (perfilAreaRsv.escrever == "1")
                                claims.Add(new Claim(ClaimTypes.Role, "USER_AREA_RSV"));
                        }
                    }
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                
                return (isLoginSuccessful, isAdmin, changePassword, null);
            }
            else
            {
                return (isLoginSuccessful, isAdmin, changePassword, "Utilizador não tem perfil para aceder à aplicação GesEDU.");
            }
        }

        public async Task SignOut()
        {
            await _httpContext.SignOutAsync();
        }

        public async Task<(bool IsSuccessful, string? ErrorMessage)> PasswordRecovery(string? email)
        {
            bool isSuccessful;
            string? errorMessage = null;

            var obj = new ResetPasswordRequest
            {
                username = email,
                email = email
            };

            var resetPasswordResult = await _genericRestRequests.Post<ResetPasswordResponse, ResetPasswordRequest>("auth", "resetPassword", obj);

            isSuccessful = resetPasswordResult != null && resetPasswordResult.messages.Any(x => x.cod_msg == "200");

            if (!isSuccessful)
                errorMessage = resetPasswordResult != null ? resetPasswordResult.messages.FirstOrDefault()?.msg : "Erro na chamada ao webservice. Tente mais tarde.";

            return (isSuccessful, errorMessage);
        }

        public async Task<(bool IsSuccessful, string? ErrorMessage)> PasswordChange(string? username, string? oldPassword, string? newPassword)
        {
            bool isSuccessful;
            string? errorMessage = null;

            var obj = new AlterarPasswordRequest
            {
                username = username,
                current_password = oldPassword,
                new_password = newPassword
            };

            var alterarPasswordResult = await _genericRestRequests.Post<AlterarPasswordResponse, AlterarPasswordRequest>("auth", "alterarPassword", obj);

            isSuccessful = alterarPasswordResult != null && alterarPasswordResult.messages.Any(x => x.cod_msg == "200");
            
            if(!isSuccessful)
                errorMessage = alterarPasswordResult != null ? alterarPasswordResult.messages.FirstOrDefault()?.msg : "Erro na chamada ao webservice. Tente mais tarde.";

            return (isSuccessful, errorMessage);
        }

        public async Task<List<GetUoResponse>?> GetUo(string? idServico)
        {
            var headers = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(idServico))
                headers.Add("Id_servico", idServico);

            return await _genericRestRequests.Get<List<GetUoResponse>>("manuais", "getUO", headers);
        }

        public async Task SetUo(GetUoResponse model, ClaimsPrincipal principal)
        {
            var clone = principal.Clone();
            var newIdentity = clone.Identity as ClaimsIdentity;

            var claimsToRemove = newIdentity?.FindAll(x => x.Type == "COD_SERVICO" || x.Type == "NOME_SERVICO" || x.Type == "NIF_SERVICO").ToList();
            
            foreach (var claim in claimsToRemove)
            {
                newIdentity.RemoveClaim(claim);
            }

            newIdentity?.AddClaims(new List<Claim> {
                new Claim("COD_SERVICO", model.Cod_agrupamento),
                new Claim("NOME_SERVICO", model.Nome),
                new Claim("NIF_SERVICO", model.Nif_servico.ToString())
            });

            await _httpContext.SignOutAsync();

            await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(newIdentity));
        }
    }
}
