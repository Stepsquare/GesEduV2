using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetManuaisApreciadosResponseItem
    {
        public string? id_ano_letivo { get; set; }
        public string? des_ano_letivo { get; set; }
        public string? des_disciplina { get; set; }
        public int ano_escolar { get; set; }
        public string? des_ano_escolar { get; set; }
        public int id_manual { get; set; }
        public string? titulo { get; set; }
        public string? isbn { get; set; }
        public string? editora { get; set; }
        public decimal preco { get; set; }
        public bool apreciado { get; set; }
        public bool certificado { get; set; }
    }
}
