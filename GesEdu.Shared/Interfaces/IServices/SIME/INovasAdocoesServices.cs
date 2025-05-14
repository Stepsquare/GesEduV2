using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Shared.Interfaces.IServices.SIME
{
    public interface INovasAdocoesServices : ISIMEBaseServices
    {
        Task<GetManuaisAdotadosListagemResponse?> GetManuaisAdotadosPdfExport(string cod_escola_me, string tipologia);
        Task<GetManuaisAdotadosResponse.GetManuaisAdotadosObject?> GetManuaisAdotados(string ciclo);
        Task<List<GenericPostResponse.Message>?> SetManuaisEscola(SetManuaisEscolaRequest requestObj);
        Task<List<GenericPostResponse.Message>?> DelManuaisEscola(int id_manual);
    }
}
