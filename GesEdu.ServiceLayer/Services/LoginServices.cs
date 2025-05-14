using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IServices;
using GesEdu.Shared.WebserviceModels.Auth;
using System.Security.Claims;
using GesEdu.Shared.WebserviceModels.Manuais;
using GesEdu.Shared.ExceptionHandler.Exceptions;
using System.Net;
using System.Net.Http.Json;
using GesEdu.Shared.Resources;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using GesEdu.Shared.Extensions;
using Newtonsoft.Json;
using System.Collections;
using System.Security.Principal;
using GesEdu.Shared.WebserviceModels;

namespace GesEdu.ServiceLayer.Services
{
    public class LoginServices(
        IHttpContextAccessor httpContextAccessor, 
        IHttpClientFactory httpClientFactory, 
        IUnitOfWork unitOfWork, 
        IHostEnvironment environment) : BaseServices(httpContextAccessor, httpClientFactory, unitOfWork, environment), ILoginServices
    {
        public async Task<(List<Claim> claims, bool chooseUo, bool changePassword)> SignIn(string username, string password)
        {
            bool changePassword = false;
            bool chooseUo = false;

            var obj = new LoginUtilizadorRequest
            {
                username = username,
                password = password
            };

            var loginUtilizadorRequest = new HttpRequestMessage(HttpMethod.Post, "auth/loginUtilizador");

            loginUtilizadorRequest.Content = JsonContent.Create(obj);

            var loginUtilizadorResponse = await SendAsync<LoginUtilizadorResponse>(loginUtilizadorRequest);

            //TODO - Rever validação de entrada no GesEDU... Perfil generico de entrada será o CODE, perfil ...
            if (loginUtilizadorResponse!.cod_origem == "PUB"
                    || !loginUtilizadorResponse.perfis.Any(x => x.cod_perfil!.Contains("APP_EXTERNA_GES_EDU")))
                throw new WebserviceException(HttpStatusCode.Unauthorized, "Utilizador não sem perfil de acesso.");

            var getFaseRequest = new HttpRequestMessage(HttpMethod.Get, "manuais/getFase");

            var getFaseResponse = await SendAsync<GetFaseResponse>(getFaseRequest);

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
                    new Claim("SIME_ANO_LETIVO", getFaseResponse.sime_id_ano_letivo_atual!),
                    new Claim("SIME_DES_ANO_LETIVO", getFaseResponse.sime_des_id_ano_letivo_atual!),
                    new Claim("ESTADO_FASE", getFaseResponse.cod_estado_fase!),
                    new Claim("COD_ORIGEM", loginUtilizadorResponse.cod_origem!),
                    new Claim("NOME_ORIGEM", loginUtilizadorResponse.nome_origem!),
            };

            if (!string.IsNullOrEmpty(getFaseResponse.sime_id_ano_letivo_anterior))
            {
                claims.Add(new Claim("SIME_ANO_LETIVO_ANTERIOR", getFaseResponse.sime_id_ano_letivo_anterior!));
                claims.Add(new Claim("SIME_DES_ANO_LETIVO_ANTERIOR", getFaseResponse.sime_des_id_ano_letivo_anterior!));
            }

            //TODO - Descomentar validação de Perfil ADMIN
            if (true || loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == GesEduProfiles.ADMIN))
            {
                claims.Add(new Claim(ClaimTypes.Role, GesEduProfiles.ADMIN));
                claims.Add(new Claim("ID_SERVICO_ORIGEM", loginUtilizadorResponse.id_servico!));
            }
            else
            {
                if (loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == GesEduProfiles.SIME_DGE))
                {
                    //O perfil SIME da DGE usa o mesmo sistema de selecção de UO que os Admins...
                    claims.Add(new Claim("ID_SERVICO_ORIGEM", loginUtilizadorResponse.id_servico!));
                }
                else
                {
                    claims.Add(new Claim("ID_SERVICO", loginUtilizadorResponse.id_servico!));
                    claims.Add(new Claim("COD_SERVICO", loginUtilizadorResponse.cod_servico!));
                    claims.Add(new Claim("NOME_SERVICO", loginUtilizadorResponse.nome_servico!));
                    claims.Add(new Claim("NIF_SERVICO", loginUtilizadorResponse.nif_servico!));
                }

                //Perfil de gestão de Utilizadores
                if (loginUtilizadorResponse.responsavel == "S")
                    claims.Add(new Claim(ClaimTypes.Role, "USER_MANAGER"));

                //Perfil MEGA
                if (loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == GesEduProfiles.MEGA))
                    claims.Add(new Claim(ClaimTypes.Role, GesEduProfiles.MEGA));

                //Perfil Area Reservada
                if (loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == GesEduProfiles.AREA_RESERVADA))
                {
                    claims.Add(new Claim(ClaimTypes.Role, GesEduProfiles.AREA_RESERVADA));

                    if (loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == GesEduProfiles.REACT))
                        claims.Add(new Claim(ClaimTypes.Role, GesEduProfiles.REACT));
                }

                //Perfil SIME para DGE
                if (loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == GesEduProfiles.SIME_DGE))
                    claims.Add(new Claim(ClaimTypes.Role, GesEduProfiles.SIME_DGE));

                //Perfil SIME para Escolas
                if (loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == GesEduProfiles.SIME_ESC))
                    claims.Add(new Claim(ClaimTypes.Role, GesEduProfiles.SIME_ESC));
            }

            changePassword = loginUtilizadorResponse.trocar_password == "S";
            chooseUo = loginUtilizadorResponse.perfis.Any(x => x.cod_perfil == GesEduProfiles.ADMIN || x.cod_perfil == GesEduProfiles.SIME_DGE);

            return (claims, chooseUo, changePassword);
        }

        public async Task<List<GenericPostResponse.Message>?> PasswordRecovery(string? email)
        {
            var obj = new ResetPasswordRequest
            {
                username = email,
                email = email
            };

            var resetPasswordRequest = new HttpRequestMessage(HttpMethod.Post, "auth/resetPassword");

            resetPasswordRequest.Content = JsonContent.Create(obj);

            var resetPasswordResponse = await SendAsync<ResetPasswordResponse>(resetPasswordRequest);

            return resetPasswordResponse?.messages;
        }

        public async Task<List<GenericPostResponse.Message>?> PasswordChange(string? username, string? oldPassword, string? newPassword)
        {
            var obj = new AlterarPasswordRequest
            {
                username = username,
                current_password = oldPassword,
                new_password = newPassword
            };

            var alterarPasswordRequest = new HttpRequestMessage(HttpMethod.Post, "auth/alterarPassword");

            alterarPasswordRequest.Content = JsonContent.Create(obj);

            var alterarPasswordResponse = await SendAsync<AlterarPasswordResponse>(alterarPasswordRequest);

            return alterarPasswordResponse?.messages;
        }

        public async Task<List<GetUoResponseItem>?> GetUo()
        {
            var getUoRequest = new HttpRequestMessage(HttpMethod.Get, "manuais/getUO");

            getUoRequest.Headers.Add("id_servico", _httpContext.User.GetIdServicoOrigem());

            return await SendAsync<List<GetUoResponseItem>>(getUoRequest);
        }

        public ClaimsPrincipal SetUo(GetUoResponseItem model, ClaimsPrincipal principal)
        {
            var clone = principal.Clone();
            var newIdentity = clone.Identity as ClaimsIdentity;

            var claimsToRemove = newIdentity?.FindAll(x => x.Type == "ID_SERVICO" 
                                                        || x.Type == "COD_SERVICO" 
                                                        || x.Type == "NOME_SERVICO" 
                                                        || x.Type == "NIF_SERVICO" 
                                                        || x.Type == "DIRETOR_SERVICO"
                                                        || x.Type == "CONTACTOS_UO").ToList();

            foreach (var claim in claimsToRemove!)
            {
                newIdentity!.RemoveClaim(claim);
            }

            newIdentity?.AddClaims(new List<Claim> {
                new Claim("ID_SERVICO", model.Id_servico.ToString()),
                new Claim("COD_SERVICO", model.Cod_agrupamento!),
                new Claim("NOME_SERVICO", model.Nome!),
                new Claim("NIF_SERVICO", model.Nif_servico.ToString()),
                new Claim("DIRETOR_SERVICO", model.Diretor!),
                new Claim("CONTACTOS_UO", JsonConvert.SerializeObject(model.Contactos))
            });

            var newClaimsPrincipal = new ClaimsPrincipal(newIdentity!);

            return newClaimsPrincipal;
        }
    }
}
