using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetManuaisAdotadosUoResponse
    {
        public string? id_ano_letivo { get; set; }
        public string? des_ano_letivo { get; set; }
        public string? cod_uo { get; set; }
        public string? nome { get; set; }
        public bool est_estimativa { get; set; }
        public List<Manual>? manuais { get; set; } = [];

        public class Manual
        {
            public int id_manual { get; set; }
            public int id_manual_escola { get; set; }
            public int cod_escola_me { get; set; }
            public string? nome_escola { get; set; }
            public string? ano_escolar { get; set; }
            public string? des_ano_escolar { get; set; }
            public string? titulo { get; set; }
            public string? isbn { get; set; }
            public string? editora { get; set; }
            public decimal preco { get; set; }
            public int id_disciplina { get; set; }
            public string? des_disciplina { get; set; }
            public string? cod_certificacao { get; set; }
            public string? des_certificacao { get; set; }
            public int? num_alunos { get; set; }
            public bool adquirir_manual { get; set; }
            public bool atualizado { get; set; }
            public string? adotado_em { get; set; }
        }
    }
}
