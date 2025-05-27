using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Areas.SIME.Models
{
    public class NovaAdocaoModalViewModel
    {
        public List<GetManuaisEscolaResponseItem> Manuais { get; set; } = [];
        public List<GetEscolasCiclosResponse.Escola> Escolas { get; set; } = [];
    }
}
