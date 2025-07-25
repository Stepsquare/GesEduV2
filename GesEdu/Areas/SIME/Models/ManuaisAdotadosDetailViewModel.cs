using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Areas.SIME.Models
{
    public class ManuaisAdotadosDetailViewModel
    {
        public int TotalEscolasCiclo { get; set; }
        public string? Ciclo { get; set; }
        public string? AnoEscolar { get; set; }
        public int DisciplinaId { get; set; }
        public string? DisciplinaDesc { get; set; }
        public List<GetManuaisAdotadosResponse.GetManuaisAdotadosObject.AnoEscolar.Disciplina.Manual> Manuais { get; set; } = [];
    }
}
