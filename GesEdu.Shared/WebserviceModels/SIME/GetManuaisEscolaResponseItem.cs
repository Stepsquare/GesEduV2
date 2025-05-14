using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetManuaisEscolaResponseItem
    {
        public string? cod_uo { get; set; }
        public string? cod_escola_me { get; set; }
        public int id_manual { get; set; }
        public int id_disciplina { get; set; }
        public string? des_disciplina { get; set; }
        public string? ano_escolar { get; set; }
        public string? des_ano_escolar { get; set; }
        public string? titulo { get; set; }
        public string? isbn { get; set; }
        public string? editora { get; set; }
        public decimal preco { get; set; }
    }
}
