using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.Auth
{
    public class GetUtilizadoresResponseItem
    {
        public int ID_UTILIZADOR { get; set; }
        public string? RESPONSAVEL { get; set; }
        public string? EMAIL { get; set; }
        public DateTime DT_INICIAL { get; set; }
        public DateTime DT_FINAL { get; set; }
        public string? NOME { get; set; }
        public string? ESTADO { get; set; }
    }
}
