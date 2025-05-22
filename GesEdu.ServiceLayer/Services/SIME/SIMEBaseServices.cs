using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IServices.SIME;
using GesEdu.Shared.WebserviceModels.SIME;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace GesEdu.ServiceLayer.Services.SIME
{
    public class SIMEBaseServices(
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory,
        IUnitOfWork unitOfWork,
        IHostEnvironment environment) : BaseServices(httpContextAccessor, httpClientFactory, unitOfWork, environment), ISIMEBaseServices
    {
        public async Task<List<GetAnosEscolaresResponseItem>?> GetAnoEscolares(string tipo_acao, string ano_letivo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getAnosEscolares");

            if (_httpContext.User.IsEscolaPrivada())
            {
                request.Headers.Add("cod_uo", "0");
                request.Headers.Add("cod_escola_me", _httpContext.User.GetCodigoServico());
            }
            else
            {
                request.Headers.Add("cod_uo", _httpContext.User.GetCodigoServico());
            }

            request.Headers.Add("tipo_acao", tipo_acao);
            request.Headers.Add("id_ano_letivo", ano_letivo);

            return await SendAsync<List<GetAnosEscolaresResponseItem>>(request);
        }

        public async Task<List<GetDisciplinasAnoEscResponseItem>?> GetDisciplinas(string tipo_acao, string ano_letivo, string ano_escolar, string tipologia)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getDisciplinasAnoEsc");

            if (_httpContext.User.IsEscolaPrivada())
            {
                request.Headers.Add("cod_uo", "0");
                request.Headers.Add("cod_escola_me", _httpContext.User.GetCodigoServico());
            }
            else
            {
                request.Headers.Add("cod_uo", _httpContext.User.GetCodigoServico());
            }

            request.Headers.Add("tipo_acao", tipo_acao);
            request.Headers.Add("id_ano_letivo", ano_letivo);
            request.Headers.Add("ano_escolar", ano_escolar);
            request.Headers.Add("tipologia", tipologia);

            return await SendAsync<List<GetDisciplinasAnoEscResponseItem>>(request);
        }

        public async Task<List<GetCiclosUOResponseItem>?> GetCiclos()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getCiclosUO");

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

            return await SendAsync<List<GetCiclosUOResponseItem>>(request);
        }

        public async Task<List<GetEscolasResponse.Escola>?> GetEscolas()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getEscolas");

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

            var result = await SendAsync<GetEscolasResponse>(request);

            return result?.escolas;
        }

        public async Task<List<GetEscolasCiclosResponse.Escola>?> GetEscolasCiclos(string ciclo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getEscolasCiclos");

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

            request.Headers.Add("ciclo", ciclo);

            var result = await SendAsync<GetEscolasCiclosResponse>(request);

            return result?.escolas;
        }

        public async Task<List<GetManuaisEscolaResponseItem>?> GetManuaisEscola(string tipo_acao, string ano_escolar, string id_disciplina)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "sime/getManuaisEscola");

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

            request.Headers.Add("tipo_acao", tipo_acao);
            request.Headers.Add("ano_escolar", ano_escolar);
            request.Headers.Add("id_disciplina", id_disciplina);

            return await SendAsync<List<GetManuaisEscolaResponseItem>>(request);
        }
    }
}
