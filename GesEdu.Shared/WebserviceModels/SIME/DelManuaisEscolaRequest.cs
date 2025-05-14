using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class DelManuaisEscolaRequest
    {
        public string? cod_uo { get; set; }
        public string? cod_escola_me { get; set; }
        public int id_manual { get; set; }
        public string? id_ano_letivo { get; set; }
        public string? utilizador { get; set; }
    }
}
