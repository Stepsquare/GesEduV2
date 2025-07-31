using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams.SIME;
using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Shared.Interfaces.IServices.SIME
{
    public interface IPedidosExcecaoServices :ISIMEBaseServices
    {
        Task<List<GetDisciplinasPLNMResponse.Disciplina>> GetDisciplinasPLNM(string tipoEnsino);
        Task<PaginatedResult<GetPedidosExcecaoResponseItem>> GetPedidosExcecao(GetPedidosExcecaoParams filter);
        Task<SetPedExcecaoResponse?> SetPedExcecao(SetPedExcecaoRequest requestObj);
    }
}
