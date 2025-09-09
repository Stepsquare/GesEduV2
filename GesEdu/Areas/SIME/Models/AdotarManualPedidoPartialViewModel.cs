using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Areas.SIME.Models
{
    public class AdotarManualPedidoPartialViewModel
    {
        public List<GetManuaisEscolaResponseItem> Manuais { get; set; } = [];
        public List<GetEscolasCiclosResponse.Escola> Escolas { get; set; } = [];

        public string? IdPedido { get; set; }
    }
}
