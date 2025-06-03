using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams.SIME;
using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Shared.Interfaces.IServices.SIME
{
    public interface IAdocoesAnosAnterioresServices : ISIMEBaseServices
    {
        Task<PaginatedResult<GetManuaisAdotadosUoResponse.Manual>> GetManuaisAdotadosUo(GetManuaisAdotadosUoParams filter);
        Task<List<GenericPostResponse.Message>?> SetNumAlunos(SetNumAlunosRequest requestObj);
    }
}
