using GesEdu.Shared.WebserviceModels.Adm;
using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Areas.SIME.Models
{
    public class PedidoExcecaoDetalheViewModel
    {
        public GetPedExcecaoResponse? Detalhe { get; set; }
        public List<GetDominiosResponseItem> TiposAnexo { get; set; } = [];
        public List<GetAnosEscolaresResponseItem> AnosEscolaridade { get; set; } = [];
    }
}
