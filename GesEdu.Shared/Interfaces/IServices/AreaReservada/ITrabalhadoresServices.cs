using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams.AreaReservada;
using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.AreaReservada.Cadastro;

namespace GesEdu.Shared.Interfaces.IServices.AreaReservada
{
    public interface ITrabalhadoresServices
    {
        Task<PaginatedResult<GetFuncionariosUoRespResponse.Funcionario>> GetFuncionariosUoResp(GetFuncionariosUoRespParams filter);
        Task<GetFuncionariosUoRespResponse.Funcionario?> GetFuncionarioUoResp(string nif);
        Task<List<GetDocenteHabilitacaoResponseItem>?> GetDocenteHabilitacao(string id_pessoa_unica);
        Task<GetDocenteHabilitacaoResponseItem?> GetDocenteHabilitacao(string id_pessoa_unica, int id_habilitacao);
        Task<List<GetDocenteFormacaoResponseItem>?> GetDocenteFormacao(string id_pessoa_unica);
        Task<GetDocenteFormacaoResponseItem?> GetDocenteFormacao(string id_pessoa_unica, int id_formacao);
        Task<GetAnexoPessoaResponse?> GetAnexoPessoa(string id_anexo_pessoa);
        Task<List<GenericPostResponse.Message>?> SetDocenteEstadoHabilitacao(SetDocenteEstadoHabilitacaoRequest requestObj);
        Task<List<GenericPostResponse.Message>?> SetDocenteEstadoFormacao(SetDocenteEstadoFormacaoRequest requestObj);
    }
}
