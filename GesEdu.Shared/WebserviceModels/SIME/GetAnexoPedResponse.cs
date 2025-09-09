using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetAnexoPedResponse
    {
        public int id_pedido_cab { get; set; }
        public int coduo { get; set; }
        public int id_agrupamento { get; set; }
        public int cod_escola_me { get; set; }
        public int id_escola { get; set; }
        public int id_ano_letivo { get; set; }
        public int id_disciplina { get; set; }
        public int ano_escolar { get; set; }
        public int id_anexo { get; set; }
        public int tipo_anexo { get; set; }
        public string? des_anexo { get; set; }
        public string? dt_anexo { get; set; }
        public string? utilizador { get; set; }
        public string? filename { get; set; }
        public string? mimetype { get; set; }
        public string? anexo_base64 { get; set; }
    }
}
