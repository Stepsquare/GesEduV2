using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Shared.Interfaces.IServices.SIME
{
    public interface INovasAdocoesServices : ISIMEBaseServices
    {
        Task<GetManuaisAdotadosListagemResponse?> GetManuaisAdotadosPdfExport(string cod_escola_me, string tipologia);
    }
}
