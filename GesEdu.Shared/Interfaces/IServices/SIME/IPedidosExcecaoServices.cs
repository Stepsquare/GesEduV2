using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams.SIME;
using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.SIME;
using Microsoft.AspNetCore.Http;

namespace GesEdu.Shared.Interfaces.IServices.SIME
{
    public interface IPedidosExcecaoServices :ISIMEBaseServices
    {
        Task<PaginatedResult<GetPedidosExcecaoResponseItem>> GetPedidosExcecao(GetPedidosExcecaoParams filter);
        Task<GetPedExcecaoResponse?> GetPedExcecao(string idPedido, string? anoLetivo = null);
        Task<SetPedExcecaoResponse?> SetPedExcecao(SetPedExcecaoRequest requestObj);
        Task<List<GenericPostResponse.Message>?> UpdPedExcecao(UpdPedExcecaoRequest requestObj);
        Task<List<GenericPostResponse.Message>?> SetPedEstado(SetPedEstadoRequest requestObj);
        Task<List<GenericPostResponse.Message>?> DelManualExcecao(DelManualExcecaoRequest requestObj);
        Task<GetAnexoPedResponse?> GetAnexoPed(string idPedido, string idAnexo, string tipoAnexo);
        Task<List<GenericPostResponse.Message>?> SetAnexoPed(IFormFile file, int idPedido, int tipoAnexo);
        Task<List<GenericPostResponse.Message>?> DelAnexoPed(int idPedido, int idAnexo, int tipoAnexo);
        Task<List<GetDisciplinasPLNMResponse.Disciplina>?> GetDisciplinasPLNM(string tipoEnsino);
        Task<List<GetManuaisEscolaResponseItem>?> GetManuaisEscolaPLNM(string codAgrDisc, string anoEscolar);
    }
}
