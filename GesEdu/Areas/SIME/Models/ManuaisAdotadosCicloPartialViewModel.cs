using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Areas.SIME.Models
{
    public class ManuaisAdotadosCicloPartialViewModel
    {
        public int TotalEscolasCiclo { get; set; }
        public required GetManuaisAdotadosResponse.GetManuaisAdotadosObject ManuaisAdotados { get; set; }
    }
}
