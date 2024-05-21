using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.Auth
{
    public class AlterarEstadoUtilizadorRequest
    {
        public int id_utilizador { get; set; }
        public string? id_servico { get; set; }
        public string? estado { get; set; }
    }
}
