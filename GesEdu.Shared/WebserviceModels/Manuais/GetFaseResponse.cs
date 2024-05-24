using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.Manuais
{
    public class GetFaseResponse
    {
        public int id_fase { get; set; }
        public string? cod_fase { get; set; }
        public string? id_ano_letivo_atual { get; set; }
        public string? des_id_ano_letivo_atual { get; set; }
        public string? id_ano_letivo_anterior { get; set; }
        public string? des_id_ano_letivo_anterior { get; set; }
        public string? sime_id_ano_letivo_atual { get; set; }
        public string? sime_des_id_ano_letivo_atual { get; set; }
        public string? sime_id_ano_letivo_anterior { get; set; }
        public string? sime_des_id_ano_letivo_anterior { get; set; }
        public string? cod_estado_fase { get; set; }
    }
}
