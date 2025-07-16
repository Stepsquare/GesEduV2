using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams.SIME;
using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Shared.Interfaces.IServices.SIME
{
    public interface IManuaisAdaptadosServices : ISIMEBaseServices
    {
        Task<PaginatedResult<GetAlunosManuaisAdaptadosResponse.AlunoManuaisAdaptados>> GetAlunosManuaisAdaptados(GetAlunosManuaisAdaptadosParams filter);
        Task<GetDetAlunosManAdaptadosResponse?> GetDetAlunosManAdaptados(int id_aluno);
        Task<GetIdentificaAlunoManAdaptResponse?> GetIdentificaAlunoManAdapt(string tipoDoc, string numDoc);
        Task<GetManuaisAnoEscolarResponse?> GetManuaisAnoEscolar(int idAluno, int anoEscolar);
        Task<List<GenericPostResponse.Message>?> SetAlunosManuaisAdaptados(SetAlunosManuaisAdaptadosRequest requestObj);
    }
}
