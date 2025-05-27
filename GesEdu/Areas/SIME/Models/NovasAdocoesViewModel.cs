using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Areas.SIME.Models
{
    public class NovasAdocoesViewModel
    {
        public List<GetCiclosUOResponseItem> Ciclos { get; set; } = [];
        public List<GetEscolasResponse.Escola> Escolas { get; set; } = [];
    }
}
