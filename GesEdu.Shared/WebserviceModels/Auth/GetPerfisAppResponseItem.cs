using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.Auth
{
    public class GetPerfisAppResponseItem
    {
        public int ID_PERFIL { get; set; }
        public string? CODIGO { get; set; }
        public string? DESCRICAO { get; set; }
        public string? DT_INICIAL { get; set; }
    }
}
