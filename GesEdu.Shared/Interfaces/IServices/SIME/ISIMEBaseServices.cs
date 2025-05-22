using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Shared.Interfaces.IServices.SIME
{
    public interface ISIMEBaseServices
    {
        Task<List<GetAnosEscolaresResponseItem>?> GetAnoEscolares(string tipo_acao, string ano_letivo);
        Task<List<GetDisciplinasAnoEscResponseItem>?> GetDisciplinas(string tipo_acao, string ano_letivo, string ano_escolar, string tipologia);
        Task<List<GetCiclosUOResponseItem>?> GetCiclos();
        Task<List<GetEscolasResponse.Escola>?> GetEscolas();
        Task<List<GetEscolasCiclosResponse.Escola>?> GetEscolasCiclos(string ciclo);
        Task<List<GetManuaisEscolaResponseItem>?> GetManuaisEscola(string tipo_acao, string ano_escolar, string id_disciplina);
    }
}
