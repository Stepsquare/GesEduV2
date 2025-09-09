using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class SetAnexoPedRequest
    {
        public int id_pedido_cab { get; set; }
        public int tipo_anexo { get; set; }
        public string? filename { get; set; }
        public string? mimetype { get; set; }
        public string? anexo_base64 { get; set; }
        public string? utilizador { get; set; }
    }
}
