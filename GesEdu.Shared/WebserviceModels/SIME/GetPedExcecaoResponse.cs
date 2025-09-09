using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetPedExcecaoResponse
    {
        public int id_excecao { get; set; }
        public string? des_excecao { get; set; }
        public int id_ano_letivo { get; set; }
        public string? des_ano_letivo { get; set; }
        public int id_pedido_cab { get; set; }
        public int? cod_uo { get; set; }
        public int? cod_escola_me { get; set; }
        public int? id_disciplina { get; set; }
        public string? des_disciplina { get; set; }
        public string? ciclo { get; set; }
        public int? ano_escolar { get; set; }
        public string? des_ano_escolar { get; set; }
        public string? dt_pedido { get; set; }
        public string? justif_pedido { get; set; }
        public string? tipo_ensino { get; set; }
        public string? des_tipo_ensino { get; set; }
        public int? cod_agr_disc { get; set; }
        public int id_estado { get; set; }
        public string? des_estado { get; set; }
        public string? justif_estado { get; set; }
        public string? utilizador { get; set; }
        public string? dt_estado_alt { get; set; }
        public string? utlz_estado_alt { get; set; }

        public List<Manual> manuais { get; set; } = [];
        public List<Manual> manuais_adotados { get; set; } = [];
        public List<Anexo> anexos { get; set; } = [];

        public class Manual
        {
            public int id_pedido_man { get; set; }
            public int id_pedido_cab { get; set; }
            public int id_disciplina { get; set; }
            public string? des_disciplina { get; set; }
            public int id_manual { get; set; }
            public int ano_escolar { get; set; }
            public string? des_ano_escolar { get; set; }
            public string? editora { get; set; }
            public string? titulo { get; set; }
            public string? isbn { get; set; }
            public decimal preco { get; set; }
            public List<Escola> escolas { get; set; } = [];

            public class Escola
            {
                public int id_manual_esc { get; set; }
                public int id_pedido_man { get; set; }
                public int cod_escola_me { get; set; }
                public string? nome_escola { get; set; }
                public int num_alunos { get; set; }
                public string? adotado_em { get; set; }
            }
        }

        public class Anexo
        {
            public int id_anexo { get; set; }
            public int tipo_anexo { get; set; }
            public string? des_anexo { get; set; }
            public string? dt_anexo { get; set; }
            public string? filename { get; set; }
        }
    }
}
